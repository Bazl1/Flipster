﻿using Flipster.Modules.Users.Domain.Entities;
using Flipster.Shared.Domain.Repositories;

namespace Flipster.Modules.Users.Domain.Repositories;

public interface ILocationRepository : IRepository<Location>
{
    Location? GetByValue(string value);
}