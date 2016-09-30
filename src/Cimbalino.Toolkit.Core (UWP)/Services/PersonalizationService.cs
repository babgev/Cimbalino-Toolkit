﻿// ****************************************************************************
// <copyright file="PersonalizationService.cs" company="Pedro Lamas">
// Copyright © Pedro Lamas 2014
// </copyright>
// ****************************************************************************
// <author>Pedro Lamas</author>
// <email>pedrolamas@gmail.com</email>
// <project>Cimbalino.Toolkit.Core</project>
// <web>http://www.pedrolamas.com</web>
// <license>
// See license.txt in this solution or http://www.pedrolamas.com/license_MIT.txt
// </license>
// ****************************************************************************

using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.System.UserProfile;

namespace Cimbalino.Toolkit.Services
{
    /// <summary>
    /// Represents an implementation of the <see cref="IPersonalizationService"/>.
    /// </summary>
    public class PersonalizationService : IPersonalizationService
    {
        /// <summary>
        /// Gets a value indicating whether this instance is supported.
        /// </summary>
        /// <value>true if this instance is supported; otherwise, false.</value>
        public virtual bool IsSupported { get; } = UserProfilePersonalizationSettings.IsSupported();

        /// <summary>
        /// Sets the lock screen image.
        /// </summary>
        /// <param name="fileUri">The file URI.</param>
        /// <returns>The <see cref="Task"/> object representing the asynchronous operation.</returns>
        public virtual async Task<bool> SetLockScreenImageAsync(string fileUri)
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(fileUri));

            return await UserProfilePersonalizationSettings.Current.TrySetLockScreenImageAsync(file);
        }

        /// <summary>
        /// Sets the wallpaper image.
        /// </summary>
        /// <param name="fileUri">The file URI.</param>
        /// <returns>The <see cref="Task"/> object representing the asynchronous operation.</returns>
        public virtual async Task<bool> SetWallpaperImageAsync(string fileUri)
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(fileUri));

            return await UserProfilePersonalizationSettings.Current.TrySetWallpaperImageAsync(file);
        }
    }
}