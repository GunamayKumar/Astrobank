#!/bin/bash
entities=("ChartType" "ChartPermission" "EventType" "HelpCategory" "TagCategory" "Country")

for entity in "${entities[@]}"; do
    if [ "$entity" = "Country" ]; then
        dir="src/Astrobank.Web/Areas/Admin/Views/Countries"
        cmd_ns="Astrobank.Application.MasterData.Countries.Commands"
        dto_ns="Astrobank.Application.MasterData.Countries.DTOs"
        query_ns="Astrobank.Application.MasterData.Countries.Queries.ListCountries"
        ctrl="Countries"
        model="CountryDto"
    else
        dir="src/Astrobank.Web/Areas/Admin/Views/${entity}s"
        cmd_ns="Astrobank.Application.MasterData.${entity}s.Commands"
        dto_ns="Astrobank.Application.MasterData.${entity}s.DTOs"
        query_ns="Astrobank.Application.MasterData.${entity}s.Queries.List${entity}s"
        ctrl="${entity}s"
        model="${entity}Dto"
    fi

    # Index View
    cat << IDX_EOF > "$dir/Index.cshtml"
@model $query_ns.PaginatedList<$dto_ns.$model>
@{
    ViewData["Title"] = "$entity Management";
}

<div class="d-flex justify-content-between align-items-center mb-3">
    <h2>$entity Management</h2>
    <a asp-action="Create" class="btn btn-primary">Create New</a>
</div>

@if (TempData["SuccessMessage"] != null)
{
    <div class="alert alert-success alert-dismissible fade show" role="alert">
        @TempData["SuccessMessage"]
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>
}

<form method="get" class="mb-4">
    <div class="input-group">
        <input type="text" name="searchTerm" class="form-control" placeholder="Search..." value="@ViewData["SearchTerm"]" />
        <button class="btn btn-outline-secondary" type="submit">Search</button>
        <a asp-action="Index" class="btn btn-outline-secondary">Clear</a>
    </div>
</form>

<div class="table-responsive">
    <table class="table table-striped table-hover">
        <thead class="table-light">
            <tr>
$(if [ "$entity" = "Country" ]; then echo "                <th>Country Name</th><th>ISO2</th><th>ISO3</th><th>Phone</th>"; else echo "                <th>Name</th><th>Description</th>"; fi)
$(if [ "$entity" = "EventType" ]; then echo "                <th>Category</th>"; fi)
                <th>Order</th>
                <th>Status</th>
                <th>Actions</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var item in Model.Items)
            {
                <tr>
$(if [ "$entity" = "Country" ]; then echo "                    <td>@item.CountryName</td><td>@item.ISOCode2</td><td>@item.ISOCode3</td><td>@item.PhoneCode</td>"; else echo "                    <td>@item.Name</td><td>@item.Description</td>"; fi)
$(if [ "$entity" = "EventType" ]; then echo "                    <td>@item.Category</td>"; fi)
                    <td>@item.DisplayOrder</td>
                    <td>
                        @if(item.IsActive){ <span class="badge bg-success">Active</span> }
                        else { <span class="badge bg-danger">Inactive</span> }
                    </td>
                    <td>
                        <a asp-action="Edit" asp-route-id="@item.${entity}ID" class="btn btn-sm btn-outline-primary">Edit</a>
                    </td>
                </tr>
            }
            @if(!Model.Items.Any()) {
                <tr><td colspan="10" class="text-center">No records found.</td></tr>
            }
        </tbody>
    </table>
</div>

<nav aria-label="Page navigation">
    <ul class="pagination">
        <li class="page-item @(Model.PageNumber == 1 ? "disabled" : "")">
            <a class="page-link" asp-action="Index" asp-route-pageNumber="@(Model.PageNumber - 1)" asp-route-searchTerm="@ViewData["SearchTerm"]">Previous</a>
        </li>
        <li class="page-item disabled"><span class="page-link">Page @Model.PageNumber</span></li>
        <li class="page-item @(Model.Items.Count < Model.PageSize ? "disabled" : "")">
            <a class="page-link" asp-action="Index" asp-route-pageNumber="@(Model.PageNumber + 1)" asp-route-searchTerm="@ViewData["SearchTerm"]">Next</a>
        </li>
    </ul>
</nav>
IDX_EOF

    # Create View
    cat << CRT_EOF > "$dir/Create.cshtml"
@model $cmd_ns.Create${entity}.Create${entity}Command
@{
    ViewData["Title"] = "Create $entity";
}

<div class="row">
    <div class="col-md-8 mx-auto">
        <div class="card shadow-sm">
            <div class="card-header bg-primary text-white">
                <h4 class="mb-0">Create $entity</h4>
            </div>
            <div class="card-body">
                <form asp-action="Create" method="post">
                    <div asp-validation-summary="ModelOnly" class="text-danger mb-3"></div>

$(if [ "$entity" = "Country" ]; then cat << 'FORM_EOF'
                    <div class="mb-3">
                        <label asp-for="CountryName" class="form-label"></label>
                        <input asp-for="CountryName" class="form-control" />
                        <span asp-validation-for="CountryName" class="text-danger"></span>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-4">
                            <label asp-for="ISOCode2" class="form-label"></label>
                            <input asp-for="ISOCode2" class="form-control" />
                            <span asp-validation-for="ISOCode2" class="text-danger"></span>
                        </div>
                        <div class="col-md-4">
                            <label asp-for="ISOCode3" class="form-label"></label>
                            <input asp-for="ISOCode3" class="form-control" />
                            <span asp-validation-for="ISOCode3" class="text-danger"></span>
                        </div>
                        <div class="col-md-4">
                            <label asp-for="PhoneCode" class="form-label"></label>
                            <input asp-for="PhoneCode" class="form-control" />
                            <span asp-validation-for="PhoneCode" class="text-danger"></span>
                        </div>
                    </div>
FORM_EOF
else cat << 'FORM_EOF'
                    <div class="mb-3">
                        <label asp-for="Name" class="form-label"></label>
                        <input asp-for="Name" class="form-control" />
                        <span asp-validation-for="Name" class="text-danger"></span>
                    </div>
                    <div class="mb-3">
                        <label asp-for="Description" class="form-label"></label>
                        <textarea asp-for="Description" class="form-control"></textarea>
                        <span asp-validation-for="Description" class="text-danger"></span>
                    </div>
FORM_EOF
fi)
$(if [ "$entity" = "EventType" ]; then cat << 'FORM_EOF'
                    <div class="mb-3">
                        <label asp-for="Category" class="form-label"></label>
                        <input asp-for="Category" class="form-control" />
                        <span asp-validation-for="Category" class="text-danger"></span>
                    </div>
FORM_EOF
fi)
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <label asp-for="DisplayOrder" class="form-label"></label>
                            <input asp-for="DisplayOrder" class="form-control" type="number" />
                            <span asp-validation-for="DisplayOrder" class="text-danger"></span>
                        </div>
                        <div class="col-md-6 d-flex align-items-center mt-4">
                            <div class="form-check">
                                <input asp-for="IsActive" class="form-check-input" />
                                <label asp-for="IsActive" class="form-check-label"></label>
                            </div>
                        </div>
                    </div>

                    <div class="mt-4">
                        <button type="submit" class="btn btn-primary">Save</button>
                        <a asp-action="Index" class="btn btn-outline-secondary ms-2">Cancel</a>
                    </div>
                </form>
            </div>
        </div>
    </div>
</div>
@section Scripts { <partial name="_ValidationScriptsPartial" /> }
CRT_EOF

    # Edit View
    cat << EDT_EOF > "$dir/Edit.cshtml"
@model $cmd_ns.Update${entity}.Update${entity}Command
@{
    ViewData["Title"] = "Edit $entity";
}

<div class="row">
    <div class="col-md-8 mx-auto">
        <div class="card shadow-sm">
            <div class="card-header bg-primary text-white">
                <h4 class="mb-0">Edit $entity</h4>
            </div>
            <div class="card-body">
                <form asp-action="Edit" method="post">
                    <input type="hidden" asp-for="${entity}ID" />
                    <div asp-validation-summary="ModelOnly" class="text-danger mb-3"></div>

$(if [ "$entity" = "Country" ]; then cat << 'FORM_EOF'
                    <div class="mb-3">
                        <label asp-for="CountryName" class="form-label"></label>
                        <input asp-for="CountryName" class="form-control" />
                        <span asp-validation-for="CountryName" class="text-danger"></span>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-4">
                            <label asp-for="ISOCode2" class="form-label"></label>
                            <input asp-for="ISOCode2" class="form-control" />
                            <span asp-validation-for="ISOCode2" class="text-danger"></span>
                        </div>
                        <div class="col-md-4">
                            <label asp-for="ISOCode3" class="form-label"></label>
                            <input asp-for="ISOCode3" class="form-control" />
                            <span asp-validation-for="ISOCode3" class="text-danger"></span>
                        </div>
                        <div class="col-md-4">
                            <label asp-for="PhoneCode" class="form-label"></label>
                            <input asp-for="PhoneCode" class="form-control" />
                            <span asp-validation-for="PhoneCode" class="text-danger"></span>
                        </div>
                    </div>
FORM_EOF
else cat << 'FORM_EOF'
                    <div class="mb-3">
                        <label asp-for="Name" class="form-label"></label>
                        <input asp-for="Name" class="form-control" />
                        <span asp-validation-for="Name" class="text-danger"></span>
                    </div>
                    <div class="mb-3">
                        <label asp-for="Description" class="form-label"></label>
                        <textarea asp-for="Description" class="form-control"></textarea>
                        <span asp-validation-for="Description" class="text-danger"></span>
                    </div>
FORM_EOF
fi)
$(if [ "$entity" = "EventType" ]; then cat << 'FORM_EOF'
                    <div class="mb-3">
                        <label asp-for="Category" class="form-label"></label>
                        <input asp-for="Category" class="form-control" />
                        <span asp-validation-for="Category" class="text-danger"></span>
                    </div>
FORM_EOF
fi)
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <label asp-for="DisplayOrder" class="form-label"></label>
                            <input asp-for="DisplayOrder" class="form-control" type="number" />
                            <span asp-validation-for="DisplayOrder" class="text-danger"></span>
                        </div>
                        <div class="col-md-6 d-flex align-items-center mt-4">
                            <div class="form-check">
                                <input asp-for="IsActive" class="form-check-input" />
                                <label asp-for="IsActive" class="form-check-label"></label>
                            </div>
                        </div>
                    </div>

                    <div class="mt-4">
                        <button type="submit" class="btn btn-primary">Save Changes</button>
                        <a asp-action="Index" class="btn btn-outline-secondary ms-2">Cancel</a>
                    </div>
                </form>
            </div>
        </div>
    </div>
</div>
@section Scripts { <partial name="_ValidationScriptsPartial" /> }
EDT_EOF

done
