using System;
using System.Linq;
using Newtonsoft.Json;
using Xunit;

namespace CustomT {
    public class Tests {

        [Fact]
        public void Parse_PassingN_ReturnsNumber() {
            CustomT result = Parser.Parse("n");
            
            Assert.Equal(CustomTDef.number, result.Type);
        }

        [Fact]
        public void Parse_PassingS_ReturnsText() {
            CustomT result = Parser.Parse("s");

            Assert.Equal(CustomTDef.text, result.Type);
        }

        [Fact]
        public void Parse_PassingS_ReturnsBoolean() {
            CustomT result = Parser.Parse("b");

            Assert.Equal(CustomTDef.boolean, result.Type);
        }

        [Fact]
        public void Parse_PassingSimmpleArray_ReturnsObject() {
            CustomT result = Parser.Parse("[Bob:s, Nancy:n]");

            Assert.Equal(CustomTDef.tObject, result.Type);
            Assert.Equal(2, result.Children.Count);
            Assert.True(result.Children.Any(c => c.Name == "Bob"));
        }
        
        [Fact]
        public void Parse_PassingComplexArray_ReturnsObject() {
            CustomT result = Parser.Parse("[Name: [Location:s, Phone:n]]");

            Assert.Equal(CustomTDef.tObject, result.Type);
        }

        [Fact]
        public void Parse_PassingEmptyArray_ReturnsEmptyObject() {
            CustomT result = Parser.Parse("[]");

            Assert.Equal(CustomTDef.tObject, result.Type);
            Assert.Equal(0, result.Children.Count());
        }

        [Fact] void Parse_PassingEmptyString_ReturnsNull() {
            CustomT result = Parser.Parse("");

            Assert.Null(result);
        }

        [Fact]
        public void Parse_PassingInvalidFormats_ThrowsException() {
            Assert.Throws<FormatException>(() => Parser.Parse("["));
            Assert.Throws<FormatException>(() => Parser.Parse("Bob:"));
            Assert.Throws<FormatException>(() => Parser.Parse("d"));
        }
        
    }
}