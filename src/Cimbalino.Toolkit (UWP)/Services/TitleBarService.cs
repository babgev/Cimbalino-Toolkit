﻿// ****************************************************************************
// <copyright file="TitleBarService.cs" company="Pedro Lamas">
// Copyright © Pedro Lamas 2014
// </copyright>
// ****************************************************************************
// <author>Pedro Lamas</author>
// <email>pedrolamas@gmail.com</email>
// <project>Cimbalino.Toolkit</project>
// <web>http://www.pedrolamas.com</web>
// <license>
// See license.txt in this solution or http://www.pedrolamas.com/license_MIT.txt
// </license>
// ****************************************************************************

#if WINDOWS_UWP
using System;
using Windows.ApplicationModel.Core;
#elif WINDOWS_APP
using System;
using System.Linq;
using System.Reflection;
using Cimbalino.Toolkit.Helpers;
using Windows.ApplicationModel.Core;
#else
using System;
using Cimbalino.Toolkit.Helpers;
#endif

namespace Cimbalino.Toolkit.Services
{
    public class TitleBarService : ITitleBarService
    {
#if WINDOWS_UWP
        public TitleBarService()
        {
            CoreApplication.GetCurrentView().TitleBar.IsVisibleChanged += TitleBarOnIsVisibleChanged;
        }

        private void TitleBarOnIsVisibleChanged(CoreApplicationViewTitleBar sender, object args)
        {
            var eventHandler = IsVisibleChanged;
            var titleBar = CoreApplication.GetCurrentView().TitleBar;
            eventHandler?.Invoke(sender, new TitleBarIsVisibleChangedArgs(titleBar.IsVisible));
        }
#endif

        public virtual void SetExtendViewIntoTitleBar(bool extend)
        {
#if WINDOWS_UWP
            var titleBar = CoreApplication.GetCurrentView().TitleBar;
            titleBar.ExtendViewIntoTitleBar = extend;
#elif WINDOWS_APP
            var titleBar = GetTitleBar();
            if (titleBar == null)
            {
                ExceptionHelper.ThrowNotSupported();
                return;
            }

            titleBar.ExtendViewIntoTitleBar = extend;
#else
            ExceptionHelper.ThrowNotSupported();
#endif
        }

#if WINDOWS_UWP
        public event EventHandler<TitleBarIsVisibleChangedArgs> IsVisibleChanged;
#else
        public event EventHandler<TitleBarIsVisibleChangedArgs> IsVisibleChanged
        {
            add
            {
                ExceptionHelper.ThrowNotSupported();
            }
            remove
            {
            }
        }
#endif

        public virtual double Height
        {
            get
            {
#if WINDOWS_UWP
                var titleBar = CoreApplication.GetCurrentView().TitleBar;
                return titleBar.Height;
#elif WINDOWS_APP
                var titleBar = GetTitleBar();
                return titleBar == null ? ExceptionHelper.ThrowNotSupported<double>() : titleBar.Height;
#else
                return ExceptionHelper.ThrowNotSupported<double>();
#endif
            }
        }

#if WINDOWS_APP
        private static dynamic GetTitleBar()
        {
            var window = CoreApplication.GetCurrentView();
            var titleBar = window.GetType()
                                 .GetRuntimeProperties()
                                 .FirstOrDefault(x => x.Name == "TitleBar")
                                 .GetMethod
                                 .Invoke(window, null);

            return titleBar;
        }
#endif
    }
}
