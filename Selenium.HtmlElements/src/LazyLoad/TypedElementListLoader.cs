﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using HtmlElements.Extensions;
using HtmlElements.Proxy;
using OpenQA.Selenium;

namespace HtmlElements.LazyLoad
{
    internal class TypedElementListLoader<TElement> : CachingLoader<IList<TElement>>
    {
        private readonly ILoader<ReadOnlyCollection<IWebElement>> _elementListLoader;
        private readonly IPageObjectFactory _pageObjectFactory;
        private readonly IProxyFactory _proxyFactory;

        public TypedElementListLoader(
            ILoader<ReadOnlyCollection<IWebElement>> elementListLoader,
            IPageObjectFactory pageObjectFactory, 
            IProxyFactory proxyFactory,
            Boolean enableCache) : base(enableCache)
        {
            _elementListLoader = elementListLoader;
            _pageObjectFactory = pageObjectFactory;
            _proxyFactory = proxyFactory;
        }

        public override void Reset()
        {
            base.Reset();

            _elementListLoader.Reset();
        }

        protected override IList<TElement> ExecuteLoad()
        {
            return _elementListLoader.Load().Select(CreateTypedElement).ToList();
        }

        private TElement CreateTypedElement(IWebElement element, Int32 index)
        {
            return _pageObjectFactory.Create<TElement>(
                _proxyFactory.CreateElementProxy(new WebElementListItemLoader(_elementListLoader, index, element))
            );
        }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendFormat("{0} loading elements with", GetType())
                .AppendLine()
                .AppendLine(_elementListLoader.ToString().ShiftLinesToRight(2, '.'))
                .ToString();
        }
    }
}