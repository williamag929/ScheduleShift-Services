using Xunit;
using Moq; // If you are using Moq for mocking dependencies
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using AutoMapper;
using ShiftWork.Backend.Controllers;
using ShiftWork.Backend.Models;
using ShiftWork.Backend.Services;
using Microsoft.AspNetCore.Mvc;

public class AreasControllerTests
{
    private readonly AreasController _controller;
    private readonly Mock<IAreaService> _areaServiceMock = new Mock<IAreaService>();
    private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
    private readonly Mock<IMemoryCache> _memoryCacheMock = new Mock<IMemoryCache>();

    public AreasControllerTests()
    {
        _controller = new AreasController(_areaServiceMock.Object, _mapperMock.Object, _memoryCacheMock.Object);
    }

    // Add test methods for each action in AreasController
    // Example test method for GetAreas action

    [Fact]
    public async Task GetArea_Returns_OkObjectResult()
    {
        // Arrange
        var companyId = "yourCompanyId";
        var cacheKey = $"Areas_{companyId}";
        IEnumerable<Area> cachedAreas = null;
        var areaId = 1;
        var areaServiceMock = new Mock<IAreaService>();
        var mapperMock = new Mock<IMapper>();
        var memoryCacheMock = new Mock<IMemoryCache>();

        var area = new Area
        {
            AreaId = 1,
            AreaName = "Area 1",
            LocationId = 1,
            CompanyId = "1231",
            IsActive = false,
            IsDeleted = false,
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Deleted = DateTime.UtcNow
        };

        cachedAreas = new List<Area> { area };

        areaServiceMock.Setup(s => s.Get(companyId, It.IsAny<int[]>())).ReturnsAsync((IEnumerable<Area>)null);
        //areaServiceMock.Setup(x => x.Get(companyId, new[] { areaId })).ReturnsAsync(cachedAreas);
        //memoryCacheMock.Setup(mc => mc.TryGetValue(cacheKey, out cachedAreas)).Returns(false);
        memoryCacheMock
            .Setup(x => x.CreateEntry(It.IsAny<object>()))
            .Returns(Mock.Of<ICacheEntry>);


        //memoryCacheMock.Setup(x => x.TryGetValue(cacheKey, out cachedAreas)).Returns(false);
        var areasController = new AreasController(areaServiceMock.Object, mapperMock.Object, memoryCacheMock.Object);
        // Act
        var result = await areasController.GetAreas(companyId);

        // Assert
        if (result == null || result.Result == null)
        {
            Assert.Fail("Value null");
        }
        else
        {
            //var b = result.Result.;

            //Console.WriteLine(b.);
            //Assert.IsType<OkObjectResult>(result.Result);
            var okObjectResult = (OkObjectResult)result.Result;
            Assert.Equal(area, okObjectResult.Value);
        }
    }



    // Add more test methods for other actions in AreasController
}