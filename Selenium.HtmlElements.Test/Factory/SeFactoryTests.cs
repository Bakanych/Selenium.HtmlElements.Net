using System;
using System.Collections.Generic;

using NUnit.Framework;

using OpenQA.Selenium;

using Rhino.Mocks;

using Selenium.HtmlElements.Elements;
using Selenium.HtmlElements.Factory;

namespace Selenium.HtmlElements.Test.Factory {

    [TestFixture]
    public class SeFactoryTests : AssertionHelper {

        private class TestPage {

            private IList<IHtmlElement> _privateListF;

            public IList<IHtmlElement> PrivateList {
                get { return _privateListF; }
            }

            public IWebElement PublicElementP { get; private set; }

        }

        private readonly IWebDriver _mockWebDriver = MockRepository.GenerateStub<IWebDriver>();
        private readonly ISearchContext _mockSearchContext = MockRepository.GenerateStub<ISearchContext>();
        private readonly IWebElement _mockWebElement = MockRepository.GenerateStub<IWebElement>();

        public IEnumerable<TestCaseData> ConstructorArguments {
            get {
                yield return
                    new TestCaseData(typeof(Wrapper<IWebDriver>), _mockWebDriver,
                        new Wrapper<IWebDriver>(_mockWebDriver));
                yield return
                    new TestCaseData(typeof(Wrapper<ISearchContext>), _mockSearchContext,
                        new Wrapper<ISearchContext>(_mockSearchContext));
                yield return
                    new TestCaseData(typeof(Wrapper<ISearchContext>), _mockWebDriver,
                        new Wrapper<ISearchContext>(_mockWebDriver));
                yield return
                    new TestCaseData(typeof(Wrapper<ISearchContext>), _mockWebElement,
                        new Wrapper<ISearchContext>(_mockWebElement));
                yield return
                    new TestCaseData(typeof(Wrapper<IWebElement>), _mockWebElement,
                        new Wrapper<IWebElement>(_mockWebElement));
            }
        }

        [TestCaseSource("ConstructorArguments")]
        public void ShouldCreateInstanceUsingSearchContext(Type type, ISearchContext context, object expected) {
            var target = PageFactory.InitElementsIn(type, context);

            Expect(target, Is.Not.Null);
            Expect(target, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldCreateInstanceWithEmptyConstructor() {
            var page = PageFactory.InitElementsIn<TestPage>(_mockWebDriver);

            Expect(page, Is.Not.Null);
        }

        [Test]
        public void ShouldInitElements() {
            var page = PageFactory.InitElementsIn<TestPage>(_mockWebDriver);

            Expect(page.PublicElementP, Is.Not.Null);
            Expect(page.PrivateList, Is.Not.Null);
        }

    }

    internal class Wrapper<T> {

        public Wrapper(T wrapped) {
            Wrapped = wrapped;
        }

        public T Wrapped { get; private set; }

        protected bool Equals(Wrapper<T> other) {
            return Equals(Wrapped, other.Wrapped);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Wrapper<T>) obj);
        }

        public override int GetHashCode() {
            return (Wrapped != null ? Wrapped.GetHashCode() : 0);
        }

    }

}