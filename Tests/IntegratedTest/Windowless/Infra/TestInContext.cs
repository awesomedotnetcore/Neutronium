﻿using System;
using System.Threading.Tasks;
using MVVM.HTML.Core;
using MVVM.HTML.Core.Binding;

namespace IntegratedTest
{
    public class TestInContext
    {
        public string Path { get; set; }

        public Func<HTMLViewEngine, Task<IHTMLBinding>> Bind { get; set; }

        public Action<IHTMLBinding> Test { get; set; }

        public Func<IHTMLBinding, Task> TestAsync { get; set; }

        public Action<IHTMLBinding> Then { get; set; }
    }
}
