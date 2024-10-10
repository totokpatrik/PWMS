using Ardalis.Result;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PWMS.Application.Addresses.Commands.CreateAddress;

public class CreateAddressCommand : IRequest<Result<CreateAddressResponse>>
{
    [Required]
    [MaxLength(100)]
    [DataType(DataType.Text)]
    public string AddressLine { get; set; }
}
