using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternSotatek.Personal.Application.Utils;
using Xunit;

namespace InternSotatek.Personal.Application.Tests.Utils;

public class SlugHelperTest
{
    [Theory]
    [InlineData("Name", "name")]
    [InlineData("Duy-Anh", "duy-anh")]
    [InlineData("Phan Duy Anh", "phan-duy-anh")]
    public async Task StringToSlug_StringName_Success(string s, string expected)
    {
        // Act 
        var result = SlugHelper.StringToSlug(s);

        // Assert
        Assert.Equal(expected, result);
    }
}
