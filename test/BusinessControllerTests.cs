using System;
using Xunit;
using Business;
using Data.Entity;
using Data;

namespace Test
{
    public class BusinessControllerTests
    {
        private readonly IBusinessController _businessController;
        private readonly IDataController _dataController;
        
        public BusinessControllerTests()
        {
            // TODO: fix customerdb context
            _dataController = new DataController(null);
            _businessController = new BusinessController(_dataController);    
        }

        [Fact]
        public void AddCustomer_ThrowsNullRefException_WhenCustomerIsNull()
        {
            var exception = Assert.Throws<NullReferenceException>(() => _businessController.AddCustomer(null));
        }

        [Fact]
        public void AddCustomer_ThrowsArgumentException_WhenFirstNameIsNull()
        {
            var customer = new Customer { FirstName = null, LastName = "ln" }; 
            var exception = Assert.Throws<ArgumentException>(() => _businessController.AddCustomer(customer));
            Assert.True(exception.Message.Contains("FirstName"));        
        }
        
        [Fact]
        public void AddCustomer_ThrowsArgumentException_WhenLastNameIsEmpty()
        {
            var customer = new Customer { FirstName = "fn", LastName = string.Empty }; 
            var exception = Assert.Throws<ArgumentException>(() => _businessController.AddCustomer(customer));
            Assert.True(exception.Message.Contains("LastName"));        
        }

        [Fact(Skip="db context needs to be fix")]
        public void AddCustomer_CallsDataController_AddCustomer()
        {
            var customer = new Customer { FirstName = "fn", LastName = "ln" }; 
            Assert.True(_businessController.AddCustomer(customer));
        }
    }
    #region Instructions
/*
 * You are tasked with writing an algorithm that determines the value of a used car, 
 * given several factors.
 * 
 *    AGE:    Given the number of months of how old the car is, reduce its value one-half 
 *            (0.5) percent.
 *            After 10 years, it's value cannot be reduced further by age. This is not 
 *            cumulative.
 *            
 *    MILES:    For every 1,000 miles on the car, reduce its value by one-fifth of a
 *              percent (0.2). Do not consider remaining miles. After 150,000 miles, it's 
 *              value cannot be reduced further by miles.
 *            
 *    PREVIOUS OWNER:    If the car has had more than 2 previous owners, reduce its value 
 *                       by twenty-five (25) percent. If the car has had no previous  
 *                       owners, add ten (10) percent of the FINAL car value at the end.
 *                    
 *    COLLISION:        For every reported collision the car has been in, remove two (2) 
 *                      percent of it's value up to five (5) collisions.
 *                    
 * 
 *    Each factor should be off of the result of the previous value in the order of
 *        1. AGE
 *        2. MILES
 *        3. PREVIOUS OWNER
 *        4. COLLISION
 *        
 *    E.g., Start with the current value of the car, then adjust for age, take that  
 *    result then adjust for miles, then collision, and finally previous owner. 
 *    Note that if previous owner, had a positive effect, then it should be applied 
 *    AFTER step 4. If a negative effect, then BEFORE step 4.
 */
#endregion

    public class Car
    {
        public decimal PurchaseValue { get; set; }
        public int AgeInMonths { get; set; }
        public int NumberOfMiles { get; set; }
        public int NumberOfPreviousOwners { get; set; }
        public int NumberOfCollisions { get; set; }
    }

    public class PriceDeterminator
    {
        public decimal DetermineCarPrice(Car car)
        {
            var purchaseValue = car.PurchaseValue;

            var ageCalc = new AgeCalculator();

            var ageValue = ageCalc.Calculate(car.AgeInMonths, purchaseValue);
            Console.WriteLine($"age value: {ageValue}");

            purchaseValue -= ageValue;

            var mileCalc = new MileageCalculator();
            var mileValue = mileCalc.Calculate(car.NumberOfMiles, purchaseValue);
            Console.WriteLine($"mile value: {mileValue}");

            purchaseValue -= mileValue;
            
            OwnerValues ownerVal = null;
            decimal collisionValue = 0M;

            if (car.NumberOfPreviousOwners == 0)
            {
                collisionValue = CalculateCollision(car.NumberOfCollisions, purchaseValue); 
                purchaseValue -= collisionValue;

                ownerVal = CalculateOwnerValues(car.NumberOfPreviousOwners, purchaseValue);                
                purchaseValue += ownerVal.CarOwnerIncreaseValue;
            }
            else
            {
                ownerVal = CalculateOwnerValues(car.NumberOfPreviousOwners, purchaseValue);
                
                if (ownerVal.CarOwnerDecreaseValue > 0)
                    purchaseValue -= ownerVal.CarOwnerDecreaseValue;            

                collisionValue = CalculateCollision(car.NumberOfCollisions, purchaseValue);                
                purchaseValue -= collisionValue;         
            }
            
            return purchaseValue;
        }

        private decimal CalculateCollision(int numberOfCollisions, decimal purchaseValue)
        {
            var collisionCalc = new CollisionCalculator();
            return collisionCalc.Calculate(numberOfCollisions, purchaseValue);
        }

        private OwnerValues CalculateOwnerValues(int numberOfPreviousOwners, decimal purchaseValue)
        {
            var ownerCalc = new OwnerCalculator();
            return ownerCalc.Calculate(numberOfPreviousOwners, purchaseValue);                
        }
    }

    public interface IAgeCalculator
    {
        decimal Calculate(int numberOfMonths, decimal carValue);
    }

    public class AgeCalculator : IAgeCalculator
    {
        private const decimal ageFactor = 0.005M;
        private const int maxNumberOfMonths = 120; 
        public decimal Calculate(int numberOfMonths, decimal carValue)
        {
            decimal result = 0M;
            if (numberOfMonths > maxNumberOfMonths) numberOfMonths = maxNumberOfMonths; 

            result = (ageFactor * carValue) * numberOfMonths;
            return result;
        }        
    }

    public interface IMileageCalculator
    {
        decimal Calculate(int mileage, decimal carValue);
    }

    public class MileageCalculator : IMileageCalculator
    {
        private const decimal milesFactor = 0.002M;
        private const int maxNumberOfMiles = 150000;
        private const int mileRange = 1000;
         
        public decimal Calculate(int mileage, decimal carValue)
        {
            decimal result = 0M;
            if (mileage > maxNumberOfMiles) mileage = maxNumberOfMiles; 

            result = (mileage / mileRange) * milesFactor * carValue;
            return result;
        }        
    }

    public class OwnerValues
    {
        public decimal CarOwnerIncreaseValue { get; set; }
        public decimal CarOwnerDecreaseValue { get; set; }
    }

    public interface IOwnerCalculator
    {
        OwnerValues Calculate(int numberOfOwners, decimal carValue);
    }

    public class OwnerCalculator : IOwnerCalculator
    {
        private const decimal ownerFactor = 0.25M;
        private const int maxNumberOfOwners = 2;
        private const decimal noPreviousOwnerFactor = 0.10M;        

        public OwnerValues Calculate(int numberOfOwners, decimal carValue)
        {
            var ownerVal = new OwnerValues();
            if (numberOfOwners > maxNumberOfOwners)
            {
                ownerVal.CarOwnerDecreaseValue = (decimal)carValue * ownerFactor;
            }
            else if (numberOfOwners == 0)
            {
                ownerVal.CarOwnerIncreaseValue = (decimal)carValue * noPreviousOwnerFactor;
            }

            return ownerVal;
        }        
    }
    
    public interface ICollisionCalculator
    {
        decimal Calculate(int numberOfCollisions, decimal carValue);
    }
    
    public class CollisionCalculator : ICollisionCalculator
    {
        private const int maxNumberOfCollisions = 5;
        private const decimal collisionFactor = 0.02M;
        public decimal Calculate(int numberOfCollisions, decimal carValue)
        {
            if (numberOfCollisions > maxNumberOfCollisions) numberOfCollisions = maxNumberOfCollisions;

            return numberOfCollisions * collisionFactor * carValue;        
        }
    }
    public class UnitTests
    {
        [Fact]
        public void CalculateCarValue()
        {
            AssertCarValue(25313.40m, 35000m, 3 * 12, 50000,  1, 1);
            AssertCarValue(19688.20m, 35000m, 3 * 12, 150000, 1, 1);
            AssertCarValue(19688.20m, 35000m, 3 * 12, 250000, 1, 1);
            AssertCarValue(20090.00m, 35000m, 3 * 12, 250000, 1, 0);
            AssertCarValue(21657.02m, 35000m, 3 * 12, 250000, 0, 1);
        }

        private static void AssertCarValue(decimal expectValue, decimal purchaseValue, 
        int ageInMonths, int numberOfMiles, int numberOfPreviousOwners, int 
        numberOfCollisions)
        {
            Car car = new Car
                        {
                            AgeInMonths = ageInMonths,
                            NumberOfCollisions = numberOfCollisions,
                            NumberOfMiles = numberOfMiles,
                            NumberOfPreviousOwners = numberOfPreviousOwners,
                            PurchaseValue = purchaseValue
                        };
            PriceDeterminator priceDeterminator = new PriceDeterminator();
            var carPrice = priceDeterminator.DetermineCarPrice(car);
            Assert.Equal(expectValue, carPrice);
        }
    }
}

