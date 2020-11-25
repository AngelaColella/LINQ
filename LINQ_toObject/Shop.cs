using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQ_toObject
{
    public class Shop
    {
        // Create lists
        public static List<Product> CreateProductList()
        {
            var list = new List<Product>
            {
                new Product {Id=1, Name = "Telefono", UnitPrice = 300.99},
                new Product {Id=2, Name = "Computer", UnitPrice = 800},
                new Product {Id=3, Name = "tablet", UnitPrice = 550.99}
            };
            return list;
        }

        public static List<Order> CreateOrderList()
        {
            var list = new List<Order>();

            var order = new Order {
                Id = 1, 
                ProductId = 1,    
                Quantity = 4 };

            list.Add(order);

            var order1 = new Order {
                Id = 2, 
                ProductId = 2,    
                Quantity = 1};

            list.Add(order1);

            var order2 = new Order {
                Id = 3, 
                ProductId = 1,    
                Quantity = 1};

            list.Add(order2);

            return list;

            //METODO ALTERNATIVO
            //var list = new List<Order>
            //{
            //    new Order {Id=1, ProductId = 1, Quantity = 4},
            //    new Order {Id=2, ProductId = 2, Quantity = 1},
            //    new Order {Id=3, ProductId = 3, Quantity = 1}
            //};
            //return list;
        }

        public static void Execution()
        {
            var productList = CreateProductList();
            var orderList = CreateOrderList();

            foreach (var p in productList)
            {
                Console.WriteLine("ID: {0} \n Name:{1} \n Price:{2}",p.Id, p.Name, p.UnitPrice);
            }


            foreach (var o in orderList)
            {
                Console.WriteLine("Order ID: {0} \n Product ID: {1} \n Quantity:{2}", o.Id, o.ProductId, o.Quantity);
            }

            // CREAZIONE QUERY: filtra i prodotti per prezzo > 400, li selezione e li mette in una nuova lista con proprietà Name e Prezzo
            // il tipo di ritorno di Select è un IEnumerable, quindi una lista
            var list = productList // corrisponde al FROM, cioè è la sorgente dei dati
                .Where(product => product.UnitPrice > 400) 
                //product è il record. In pratica è una foreach mascherata, perchè per ogni elemento di ProductList viene verificata la condizione
                .Select(p => new { Nome = p.Name, Prezzo = p.UnitPrice});
            //il select viene fatto sulla nuova lista restituita da where. Infatti where restituisce una IEnumerable e Select vuole in ingresso prorio questo parametro


            // Aggiungo un prodotto
            productList.Add(new Product
            {
                Id = 4,
                Name = "Bici",
                UnitPrice = 500.99
            });

            // Verrà vista anche bici perchè la query non è ancora stata eseguita e viene eseguita solo in questo foreach
            Console.WriteLine("Esecuzione Differita: ");
            foreach (var p in list)
            {
                Console.WriteLine("{0} - {1}", p.Nome, p.Prezzo);
            }

            // Esecuzione immediata
            var list1 = productList
                .Where(product => product.UnitPrice > 400)
                .Select(p => new { Nome = p.Name, Prezzo = p.UnitPrice })
                .ToList();
            // ToList restituisce una lista del tipo anonimo definito da ciò che c'è nella parentesi della Select

            // se ora aggiungo un nuovo prodotto, non verrà incluso nella query perchè con ToList si forza l'esecuzione


            productList.Add(new Product
            {
                Id = 5,
                Name = "Divano",
                UnitPrice = 500
            });

            Console.WriteLine("Esecuzione immediata: ");
            foreach (var p in list)
            {
                Console.WriteLine("{0} - {1}", p.Nome, p.Prezzo);
            }


        }

        public static void Syntax() // vediamo due sintassi per fare la stessa cosa
        {
            var productList = CreateProductList();
            var orderList = CreateOrderList();

            // Method Syntax
            var methodList = productList
                .Where(product => product.UnitPrice < 600)
                .Select(p => new { Nome = p.Name, Prezzo = p.UnitPrice })
                .ToList();

            // Query Syntax 
            var queryList =
                ( from p in productList
                where p.UnitPrice < 600
                select new { Nome = p.Name, Prezzo = p.UnitPrice }).ToList();

        }

        public static void Operators()
        {
            var productList = CreateProductList();
            var orderList = CreateOrderList();

            Console.WriteLine("Lista Prodotti: ");
            foreach (var p in productList)
            {
                Console.WriteLine("ID: {0} \n Name:{1} \n Price:{2}", p.Id, p.Name, p.UnitPrice);
            }

            Console.WriteLine("Lista Ordini: ");
            foreach (var o in orderList)
            {
                Console.WriteLine("Order ID: {0} \n Product ID: {1} \n Quantity:{2}", o.Id, o.ProductId, o.Quantity);
            }


            #region ofType
            var list = new ArrayList();
            list.Add(productList);
            list.Add("Ciao!");
            list.Add(123);

            var typeQuery =
                from item in list.OfType<int>()     // dalla lista list filtriamo solo gli item che sono int
                select item;    // seleziona solo gli item filtrati

            foreach (var item in typeQuery)
            {
                Console.WriteLine(item);
            }
            #endregion

            #region Element
            Console.WriteLine("Element: ");
            String[] empty = { };
            var el = empty.First();
            Console.WriteLine(el);
            // questo codice dà errore quindi posso scrivere;

            String[] empty1 = { };
            var el1 = empty1.FirstOrDefault();
            Console.WriteLine(el1);
            // valore di default null

            int[] empty2 = { };
            var el2 = empty2.FirstOrDefault();
            Console.WriteLine(el2);
            // valore di default 0

            var p1 = productList.ElementAt(0).Name;
            Console.WriteLine(p1);
            #endregion

            #region OrderBy
            // oridnamento dei prodotti per nome (alfabetico) e prezzo (decrescente)

            //aggiungo un alttro prodotto Telefono in moda che ordini i due con lo stesso nome sulla base del prezzo
            productList.Add(new Product { Id=4, Name = "Telefono", UnitPrice=1000 });

            // metodo 1
            // N.B. bisogna necessariamente mettere var perchè oreredList non può essere tipizzata a priori.
            var orderedList =
                from p in productList
                orderby p.Name ascending, p.UnitPrice descending
                select new { Nome = p.Name, Prezzo = p.UnitPrice };

            // metodo 2
            var orderList2 = productList
                .OrderBy(p => p.Name)
                .ThenByDescending(p => p.UnitPrice)
                .Select(p => new { Nome = p.Name, Prezzo = p.UnitPrice });
            //.Reverse();

            foreach (var p in orderedList)
            {
                Console.WriteLine("{0} - {1}", p.Nome, p.Prezzo);
            }

            foreach (var p in orderList2)
            {
                Console.WriteLine("{0} - {1}", p.Nome, p.Prezzo);
            }

            #endregion

            #region Quantificatori
            
            var productWithT = productList.Any(p => p.Name.StartsWith('T'));
            // la lambda nella parentesi restituisce true se ce n'è almeno uno con la T
            // StartsWith è un metodo di string
            var allProductWhitT = productList.All(p => p.Name.StartsWith('T'));
            // true solo se tutti i prodotti iniziano con la T

            Console.WriteLine("Ci sono prodotti che iniziano per T? \n {0}", productWithT );
            Console.WriteLine("Tutti i prodotti iniziano per T? \n {0}", allProductWhitT);

            #endregion



        }
    }
}
