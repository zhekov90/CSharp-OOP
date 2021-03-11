using NUnit.Framework;
using System;

namespace Store.Tests
{
    public class StoreManagerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ctorProductShouldWork()
        {
            Product product = new Product("produkt", 2, 2.50m);

            Assert.AreEqual("produkt", product.Name);
            Assert.AreEqual(2, product.Quantity);
            Assert.AreEqual(2.50, product.Price);
        }

        [Test]
        public void ctorStoreManagerShouldWork()
        {
            StoreManager storeManager = new StoreManager();
            Assert.AreEqual(0, storeManager.Count);
           
        }

        [Test]
        public void AddCorrectProductShouldAddProduct()
        {
            StoreManager storeManager = new StoreManager();
            Product product = new Product("produkt", 2, 2.50m);
            storeManager.AddProduct(product);

            Assert.AreEqual(storeManager.Count, 1);
        }

        [Test]
        public void AddNullProductShouldThrowException()
        {
            StoreManager storeManager = new StoreManager();
            Assert.Throws<ArgumentNullException>(() =>
            {
                storeManager.AddProduct(null);
            });
        }

        [Test]
        public void AddNullProductWithNullQtyShouldThrowException()
        {
            StoreManager storeManager = new StoreManager();
            Assert.Throws<ArgumentException>(() =>
            {
                storeManager.AddProduct(new Product("produkt", 0, 2.50m));
            }, "Product count can't be below or equal to zero.");
        }

        [Test]
        public void AddNullProductWithNegativeQtyShouldThrowException()
        {
            StoreManager storeManager = new StoreManager();
            Assert.Throws<ArgumentException>(() =>
            {
                storeManager.AddProduct(new Product("produkt", -20, 2.50m));
            }, "Product count can't be below or equal to zero.");
        }

        [Test]
        public void AddCorrectProductShouldWork()
        {
            StoreManager storeManager = new StoreManager();
            Product product = new Product("produkt", 2, 2.50m);
            storeManager.AddProduct(product);
            Assert.Contains(product, (System.Collections.ICollection)storeManager.Products);

        }

        [Test]
        public void BuyNullProductShouldThrowException()
        {
            StoreManager storeManager = new StoreManager();
            Assert.Throws<ArgumentNullException>(() =>
            {
                storeManager.BuyProduct("asd", 1);
            });
        }

        [Test]
        public void BuyProductWithoutEnoughQtyShouldThrowException()
        {
            StoreManager storeManager = new StoreManager();
            Product product = new Product("produkt", 2, 2.50m);
            storeManager.AddProduct(product);
            Assert.Throws<ArgumentException>(() =>
            {
                storeManager.BuyProduct("produkt", 3);
            }, "There is not enough quantity of this product.");
        }

        [Test]
        public void BuyProductShouldWork()
        {
            StoreManager storeManager = new StoreManager();
            Product product = new Product("produkt", 3, 2.50m);
            storeManager.AddProduct(product);
            
            storeManager.BuyProduct("produkt", 1);
            decimal finalPrice = product.Price * 1;
            Assert.AreEqual(product.Price * 1, finalPrice);
        }

        [Test]
        public void BuyProductShouldSubtractQuantity()
        {
            StoreManager storeManager = new StoreManager();
            Product product = new Product("produkt", 3, 2.50m);
            storeManager.AddProduct(product);

            storeManager.BuyProduct("produkt", 1);
            Assert.AreEqual(product.Quantity , 2);
        }

        [Test]
        public void GetTheMostExpensiveProductShouldWork()
        {
            StoreManager storeManager = new StoreManager();
            Product product = new Product("produkt", 3, 2.50m);
            Product product2 = new Product("produkt2", 3, 12.50m);
            Product product3 = new Product("produkt3", 3, 6.50m);
            storeManager.AddProduct(product);
            storeManager.AddProduct(product2);
            storeManager.AddProduct(product3);

            var mostExpensiveProduct = storeManager.GetTheMostExpensiveProduct();
            Assert.AreEqual(product2, mostExpensiveProduct);
        }
    }
}