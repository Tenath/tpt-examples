using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBooks
{
    public class BooksGlobal
    {
        public static App App { get; private set; } = null;
        public static void Initialize(App app)
        {
            if (App == null) App = app;
        }
    }
}
