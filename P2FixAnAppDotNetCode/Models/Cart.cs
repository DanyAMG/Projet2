using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        /// <summary>
        /// Read-only property for display only
        /// </summary>
        public IEnumerable<CartLine> Lines => _cartLines;

        /// <summary>
        /// Private list to stock new Lines of the cart since the list Lines is on read only
        /// </summary>
        private List<CartLine> _cartLines = new List<CartLine>();
        
        /// <summary>
        /// Return the actual cartline list
        /// </summary>
        /// <returns></returns>
        private List<CartLine> GetCartLineList()
        {
            return new List<CartLine>();
        }

        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {
            var availableStock = product.Stock;

            //Call of the function  FindProductInCartLines() to search if the product is already in the Cart
            var existingProduct = FindProductInCartLines(product.Id);
            //this line use First() of the extension LINQ and search for the first product with the same product Id

                if (existingProduct == null)    //if the product is not found in the cart it's added
                {
                    if (quantity <= availableStock)
                    {
                        _cartLines.Add(new CartLine
                        {
                            Product = product,
                            Quantity = quantity
                        });
                    }
                else
                {
                    throw new Exception("StockInsufficientError");
                }
            }
                else                            //else it adds the quantity chosen
                {
                    var cartLine = _cartLines.First(cline => cline.Product.Id == existingProduct.Id);
                    if ((cartLine.Quantity + quantity) <= availableStock)
                    {
                        cartLine.Quantity += quantity;
                    }
                else
                {
                    throw new Exception("StockInsufficientError");
                }
            }
        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product) =>
            GetCartLineList().RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            //use of the method Sum() of the extension LINQ
            //do the multiplication of the quantity and the price on each lines of the cart
            //the add up all the result to return the total
            return _cartLines.Sum(cl => cl.Product.Price * cl.Quantity);
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            //do the multiplication of the quantity and the price on each lines of the cart
            //the add up all the result to return the total
            //and finally divide the total by the sum of all quantity of each line of the cart
            if (!_cartLines.Any())
                return 0.0;
            else
            {
                return _cartLines.Sum(cl => cl.Product.Price * cl.Quantity) / _cartLines.Sum(cl => cl.Quantity);
            }
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            //For each line in the cart, it compares the Id of the product added with the Id of the product in the car,
            //If the Product is already in the cart the function returns the Id of the product else it return the value null

            foreach (CartLine cartline in Lines)           
            {
                if (cartline.Product.Id == productId)
                {
                    return cartline.Product;
                }  
            }
            return null;
            
        }

        /// <summary>
        /// Get a specific cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            return Lines.ToArray()[index];
        }

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear()
        {
            //List<CartLine> cartLines = GetCartLineList();
            _cartLines.Clear();
        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
