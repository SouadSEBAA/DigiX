﻿#pragma checksum "..\..\Gate.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "90E46F742A2660B811949DC94104CC1780ACA2EBB0EA86C519B77251B0932DE8"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WpfApp2;


namespace WpfApp2 {
    
    
    /// <summary>
    /// Gate
    /// </summary>
    public partial class Gate : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\Gate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WpfApp2.Gate Outil;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\Gate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas OutilShape;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\Gate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Path path;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\Gate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContextMenu menu;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\Gate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid TopGate;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\Gate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid BottomGate;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\Gate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LeftGate;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\Gate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid RightGate;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfApp2;component/gate.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Gate.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Outil = ((WpfApp2.Gate)(target));
            return;
            case 2:
            this.OutilShape = ((System.Windows.Controls.Canvas)(target));
            return;
            case 3:
            this.path = ((System.Windows.Shapes.Path)(target));
            
            #line 15 "..\..\Gate.xaml"
            this.path.MouseMove += new System.Windows.Input.MouseEventHandler(this.path_MouseMove);
            
            #line default
            #line hidden
            return;
            case 4:
            this.menu = ((System.Windows.Controls.ContextMenu)(target));
            return;
            case 5:
            
            #line 18 "..\..\Gate.xaml"
            ((System.Windows.Controls.ListBoxItem)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.AjouterEntrée);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 19 "..\..\Gate.xaml"
            ((System.Windows.Controls.ListBoxItem)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.SupprimerEntrée);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 20 "..\..\Gate.xaml"
            ((System.Windows.Controls.ListBoxItem)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.AjouterLabel);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 21 "..\..\Gate.xaml"
            ((System.Windows.Controls.ListBoxItem)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Supprimer);
            
            #line default
            #line hidden
            return;
            case 9:
            this.TopGate = ((System.Windows.Controls.Grid)(target));
            return;
            case 10:
            this.BottomGate = ((System.Windows.Controls.Grid)(target));
            return;
            case 11:
            this.LeftGate = ((System.Windows.Controls.Grid)(target));
            return;
            case 12:
            this.RightGate = ((System.Windows.Controls.Grid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

