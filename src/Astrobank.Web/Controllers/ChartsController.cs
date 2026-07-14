using Astrobank.Application.Charts.Commands.CreateChart;
using Astrobank.Application.Charts.Commands.UpdateChart;
using Astrobank.Application.Charts.DTOs;
using Astrobank.Application.Charts.Queries.ListCharts;
using Astrobank.Application.Common.CQRS;
using Astrobank.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Astrobank.Web.Controllers;

[Authorize]
public class ChartsController : Controller
{
    private readonly IQueryHandler<ListChartsQuery, PaginatedList<ChartDto>> _listQueryHandler;
    private readonly ICommandHandler<CreateChartCommand> _createCommandHandler;
    private readonly ICommandHandler<UpdateChartCommand> _updateCommandHandler;
    private readonly UserManager<User> _userManager;

    public ChartsController(
        IQueryHandler<ListChartsQuery, PaginatedList<ChartDto>> listQueryHandler,
        ICommandHandler<CreateChartCommand> createCommandHandler,
        ICommandHandler<UpdateChartCommand> updateCommandHandler,
        UserManager<User> userManager)
    {
        _listQueryHandler = listQueryHandler;
        _createCommandHandler = createCommandHandler;
        _updateCommandHandler = updateCommandHandler;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index(string? searchTerm, int pageNumber = 1)
    {
        var query = new ListChartsQuery { SearchTerm = searchTerm, PageNumber = pageNumber };
        var result = await _listQueryHandler.HandleAsync(query);
        ViewData["SearchTerm"] = searchTerm;
        return View(result);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new CreateChartCommand());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateChartCommand command)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        command.UserID = user.UserID;

        if (!ModelState.IsValid) return View(command);

        await _createCommandHandler.HandleAsync(command);
        TempData["SuccessMessage"] = "Chart created successfully.";
        return RedirectToAction(nameof(Index));
    }
}
