﻿using System.Collections.Generic;
using System.Numerics;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Xunit;

namespace Nethereum.ABI.UnitTests
{
    public class FunctionMultidimensionalStructNonDynamicArrayDecodingTests
    {
        public partial class TestStruct : TestStructBase { }

        public class TestStructBase
        {
            [Parameter("uint256", "x", 1)]
            public virtual BigInteger X { get; set; }
            [Parameter("uint256", "y", 2)]
            public virtual BigInteger Y { get; set; }
            [Parameter("uint256", "z", 3)]
            public virtual BigInteger Z { get; set; }
        }

        public partial class GetTestStruct3dOutputDTO : GetTestStruct3dOutputDTOBase { }

        [FunctionOutput]
        public class GetTestStruct3dOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("tuple[][][]", "testStructs", 1)]
            public virtual List<List<List<TestStruct>>> TestStructs { get; set; }
        }

        /*
         function getTestStruct3d() public view returns (TestStruct[][][] memory testStructs) {
        uint256 maxX = 2;
        uint256 maxY = 2;

        testStructs = new TestStruct[][][](maxX);
        for (uint256 x; x < maxY; x++) {

            testStructs[x] = new TestStruct[][](maxY);
            for (uint256 y; y < maxY; y++) {
                 testStructs[x][y] = getTestStruct(x, y, 2);
            }
        }
    }
    
    struct TestStruct {
        uint256 x;
        uint256 y;
        uint256 z;
    }

    function getTestStruct(uint x ,uint256 y, uint256 z) public view returns (TestStruct[] memory testStruct)
    {
            testStruct = new TestStruct[](z);
            for (uint256 a = 0; a < z; a++) {
           
                testStruct[a] =   
                        TestStruct(x, 
                                  y, 
                                  a);
            
        }
    }
        */


        [Fact]
        public void ShouldDecode3dArrayWithStructsAsNonDynamic()
        {
            var output = "00000000000000000000000000000000000000000000000000000000000000200000000000000000000000000000000000000000000000000000000000000002000000000000000000000000000000000000000000000000000000000000004000000000000000000000000000000000000000000000000000000000000002600000000000000000000000000000000000000000000000000000000000000002000000000000000000000000000000000000000000000000000000000000004000000000000000000000000000000000000000000000000000000000000001200000000000000000000000000000000000000000000000000000000000000002000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001000000000000000000000000000000000000000000000000000000000000000200000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000020000000000000000000000000000000000000000000000000000000000000040000000000000000000000000000000000000000000000000000000000000012000000000000000000000000000000000000000000000000000000000000000020000000000000000000000000000000000000000000000000000000000000001000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000000000000000000000000000000000000000000000000000000000000002000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000010000000000000000000000000000000000000000000000000000000000000001";
            var functionDecoder = new FunctionCallDecoder();
            var result = functionDecoder.DecodeFunctionOutput<GetTestStruct3dOutputDTO>(output);
            Assert.Equal(1, result.TestStructs[1][1][1].Y);
            Assert.Equal(1, result.TestStructs[1][1][1].Z);
            Assert.Equal(1, result.TestStructs[1][1][1].X);

        }

       
    }
}