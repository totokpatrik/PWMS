using PWMS.Application.Addresses.Commands.Delete;
using PWMS.Application.Addresses.Repositories;
using PWMS.Application.Addresses.Specifications;
using PWMS.Domain.Addresses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMS.Application.Core.Sites.Commands.Delete;

internal class DeleteSiteCommandHandler() : IRequestHandler<DeleteSiteCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(DeleteSiteCommand request, CancellationToken cancellationToken)
    {
        // Check if current user is an owner - only owner, or admin can delete the site
        // TODO - requirement should be used

        /*
        var entity = await _addressRepository
            .SingleOrDefaultAsync(new AddressByIdSpecification(request.Id), cancellationToken)
            .ConfigureAwait(false);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Address), request.Id);
        }

        await _addressRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
        await _addressRepository.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok(entity.Id);
        */
        throw new NotImplementedException();
    }
}