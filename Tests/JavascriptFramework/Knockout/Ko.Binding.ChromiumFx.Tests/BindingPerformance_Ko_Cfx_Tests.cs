﻿using Ko.Binding.ChromiumFx.Tests.Infra;
using System.Collections.Generic;
using Tests.Universal.HTMLBindingTests;
using Xunit;
using Xunit.Abstractions;

namespace Ko.Binding.ChromiumFx.Tests
{
    [Collection("Cfx Ko Windowless Integrated")]
    public class BindingPerformance_Ko_Cfx_Tests : HTMLBindingPerformanceTests
    {
        public BindingPerformance_Ko_Cfx_Tests(CfxKoContext context, ITestOutputHelper output)
            : base(context, output, GetKoCefPerformanceExpectations())
        {
        }

        private static Dictionary<TestPerformanceKind, int> GetKoCefPerformanceExpectations()
        {
            return new Dictionary<TestPerformanceKind, int>
            {
                {TestPerformanceKind.OneTime_Collection_CreateBinding, 1500},
                {TestPerformanceKind.TwoWay_Collection_CreateBinding, 1500},
                {TestPerformanceKind.OneWay_Collection_CreateBinding, 1500},
                {TestPerformanceKind.TwoWay_Int, 3100},
                {TestPerformanceKind.TwoWay_Collection, 4700},
                {TestPerformanceKind.TwoWay_Collection_Update_From_Javascript, 150}
            };
        }
    }
}
