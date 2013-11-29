﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using OpenQA.Selenium;

namespace HtmlElements.Locators {

    internal class ElementLocator : IElementLocator {

        private readonly IEnumerable<By> _bys;
        private readonly ISearchContext _context;

        public ElementLocator(ISearchContext context, By by) : this(context, new List<By> {by}) {}

        public ElementLocator(ISearchContext context, IEnumerable<By> bys) {
            _context = context;
            _bys = bys;
        }

        public IWebElement FindElement() {
            var execptions = new List<Exception>();

            foreach (var by in _bys) {
                try {
                    return _context.FindElement(by);
                } catch (Exception ex) {
                    execptions.Add(ex);
                }
            }

            throw new NoSuchElementException(string.Format("[{0}] failed to find element", this),
                new AggregateException(execptions));
        }

        public ReadOnlyCollection<IWebElement> FindElements() {
            return _bys.SelectMany(by => by.FindElements(_context)).ToList().AsReadOnly();
        }

        public override string ToString() {
            return string.Format("By=[{0}], Context=[{1}]", string.Join(",", _bys), _context);
        }

    }

}