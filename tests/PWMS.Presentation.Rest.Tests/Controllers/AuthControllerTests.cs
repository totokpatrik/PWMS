using PWMS.Presentation.Rest.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMS.Presentation.Rest.Tests.Controllers;

[Collection(nameof(RestCollectionDefinition))]
public class AuthControllerTests
{
    private readonly RestWebApplicationFactory<Program> _factory;

    public AuthControllerTests(RestWebApplicationFactory<Program> factory) => _factory = factory;
}
