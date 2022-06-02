using FluentAssertions;
using Kitchen.UI.Data;
using Xunit;

namespace Kitchen.UI.Tests.Data
{
    public class OrderRepository_Test
    {
        [Fact]
        public void GetAll_Success_AsExpected() {
            var sut = new OrderRepository();

            var actual = sut.GetAll();

            actual.Should().HaveCount(2);
        }
    }
}
