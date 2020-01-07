using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Splitwise.Repository.Test
{
    [CollectionDefinition("Register Dependency")]
    class BootstrapFixture : ICollectionFixture<Bootstrap>
    {

    }
}
