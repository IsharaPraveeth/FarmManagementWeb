using FarmManagement.Application.Common.Configurations;
using FarmManagement.Application.Features.Fields.Queries.GetAllFields;
using FarmManagement.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using MudBlazor;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Xml.Linq;
using static MudBlazor.CategoryTypes;

namespace FarmManagement.Web.Pages
{
    public partial class Index
    {
        [Inject] private HttpClient Http { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        
        private string ApiBaseUrl;
        private bool dense = false;
        private bool hover = true;
        private bool striped = true;
        private bool bordered = false;
        private string searchString1 = "";

        private bool isEdit = false;
        private bool isDelete = false;
        private bool isAdd = true;

        private Domain.Entities.Field modelField = new Domain.Entities.Field();
        private IEnumerable<GetAllFieldsDto> fieldsList = new List<GetAllFieldsDto>();
        MudForm form;
        private bool showForm = false;


        private GetAllFieldsDto selectedItem1 = null;
        private HashSet<GetAllFieldsDto> selectedItems = new HashSet<GetAllFieldsDto>();
        FieldModelFluentValidator fieldValidator = new FieldModelFluentValidator();

        bool success;
        string[] errors = { };

        protected override async Task OnInitializedAsync()
        {
            ApiBaseUrl = Configuration["ApiSettings:BaseUrl"];
            fieldsList = await Http.GetFromJsonAsync<List<GetAllFieldsDto>>("api/Field");
        }
        private void HideForm()
        {
            showForm = !showForm;
        }
        private void EditData(int id)
        {
            showForm = true;
            isEdit = true;
            isDelete = false;
            isAdd = false;

            GetAllFieldsDto tmp = fieldsList.First(f => f.Id == id);

            modelField.Name = tmp.Name;
            modelField.Area = tmp.Area;
            modelField.CropName = tmp.CropName;
            modelField.Id = tmp.Id;
        }

        private void DeleteData(int id)
        {
            showForm = true;
            isDelete = true;
            isEdit = false;
            isAdd = false;

            GetAllFieldsDto tmp = fieldsList.First(f => f.Id == id);

            modelField.Name = tmp.Name;
            modelField.Area = tmp.Area;
            modelField.CropName = tmp.CropName;
            modelField.Id = tmp.Id;

        }

        private void Reset()
        {
            isDelete = false;
            isEdit = false;
            isAdd = true;

            modelField.Name = "";
            modelField.Area = 0;
            modelField.CropName = "";
        }
 

        private bool FilterFunc1(GetAllFieldsDto obj) => FilterFunc(obj, searchString1);

        private bool FilterFunc(GetAllFieldsDto item, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (item.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (item.CropName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if ($"{item.Area} {item.Id}".Contains(searchString))
                return true;
            return false;
        }

        private async Task Submit()
        {
            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                try
                {
                    await form.Validate();

                    if (form.IsValid)
                    {
                        var apiUrl = $"{ApiBaseUrl}";
                        var fieldData = new
                        {
                            name = modelField.Name,
                            area = modelField.Area,
                            cropName = modelField.CropName
                        };

                        var response = await Http.PostAsJsonAsync(apiUrl, fieldData);
                        await OnInitializedAsync();
                        Reset();
                        showForm = false;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("ex" + ex.Message);
                }
            }
        }

        private async Task Delete()
        {

            var options = new DialogOptions { CloseOnEscapeKey = true };
                var parameters = new DialogParameters
            {
                { nameof(DeleteDialog.ContentText), "Do you really want to delete these records? This process cannot be undone." },
                { nameof(DeleteDialog.ButtonText), "Delete" },
                { nameof(DeleteDialog.Color), Color.Error }
            };

                var dialog = DialogService.Show<DeleteDialog>("Confirm Delete", parameters, options);
                var result = await dialog.Result;

                if (result != null && !result.Canceled)
                {
                    using (var cancellationTokenSource = new CancellationTokenSource())
                    {
                        try
                        {
                            var response = await Http.DeleteAsync($"{ApiBaseUrl}/{ modelField.Id}");

                            await OnInitializedAsync();
                            Reset();
                            showForm = false;

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("ex" + ex.Message);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Delete cancelled.");
                }
        }

        private async Task Edit()
        {
            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                try
                {
                    await form.Validate();

                    if (form.IsValid)
                    {
                        var apiUrl = $"{ApiBaseUrl}";
                        var fieldData = new
                        {
                            Id = modelField.Id,
                            area = modelField.Area,
                            cropName = modelField.CropName
                        };

                        var response = await Http.PutAsJsonAsync(apiUrl, fieldData);

                       await OnInitializedAsync();

                       Reset();
                       showForm = false;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("ex" + ex.Message);
                }
            }

        }

        public class FieldModelFluentValidator : AbstractValidator<Domain.Entities.Field>
        {
            HttpClient Http = new();
            public FieldModelFluentValidator()
            {

                RuleFor(x => x.Area)
                    .NotEqual(0);

                RuleFor(x => x.Name)
                    .NotEmpty()
                    .MustAsync((Name, cancellationToken) => IsUniqueName(Name, cancellationToken))
                    .WithMessage("Name already exists.")
                    .When(x => x.Id == 0);


                RuleFor(x => x.CropName)
                    .NotEmpty()
                    .Length(1, 100);
            }

             private async Task<bool> IsUniqueName(string name, CancellationToken cancellationToken)
             {
                 if (string.IsNullOrWhiteSpace(name))
                 {
                    return true; 
                 }

                //var response = await Http.GetAsync($"{_apiBaseUrl}/{name}", cancellationToken);
                var response = await Http.GetAsync($"https://localhost:7044/api/Field/{name}", cancellationToken);

                if (response != null)
                {
                    var responseData = await response.Content.ReadAsStringAsync();

                    if (bool.Parse(responseData) == false)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    throw new HttpRequestException("Failed to validate name with the API.");
                }

             }

            public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
            {
                var result = await ValidateAsync(ValidationContext<Domain.Entities.Field>.CreateWithOptions((Domain.Entities.Field)model, x => x.IncludeProperties(propertyName)));
                if (result.IsValid)
                    return Array.Empty<string>();
                return result.Errors.Select(e => e.ErrorMessage);
            };

        }

    }
}

