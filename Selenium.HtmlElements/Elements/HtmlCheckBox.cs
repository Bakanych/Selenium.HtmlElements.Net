﻿using HtmlElements.Extensions;

using OpenQA.Selenium;

namespace HtmlElements.Elements {

    public class HtmlCheckBox : HtmlInput {

        private const string AttrChecked = "checked";

        public HtmlCheckBox(IWebElement wrapped) : base(wrapped) {}

        public bool Checked {
            get { return this.HasAttribute(AttrChecked); }
            set { this.Do(Click).Until(self => Checked == value); }
        }

    }

}