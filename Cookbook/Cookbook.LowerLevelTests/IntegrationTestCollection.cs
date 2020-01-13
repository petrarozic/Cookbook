using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Cookbook.LowerLevelTests
{
    [CollectionDefinition("Integration test collection")]
    public class IntegrationTestCollection : ICollectionFixture<IntegrationTestSetup>
    {
    }
}
