﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PWMS.Core.SharedKernel;
using System;

namespace PWMS.Core.Extensions;

public static class ServiceProviderExtensions
{
    /// <summary>
    /// Get options from the service provider.
    /// </summary>
    /// <typeparam name="TOptions">The options type.</typeparam>
    /// <param name="serviceProvider">The service provider.</param>
    /// <returns>The options.</returns>
    public static TOptions GetOptions<TOptions>(this IServiceProvider serviceProvider)
        where TOptions : class, IAppOptions =>
        serviceProvider.GetService<IOptions<TOptions>>()?.Value;
}