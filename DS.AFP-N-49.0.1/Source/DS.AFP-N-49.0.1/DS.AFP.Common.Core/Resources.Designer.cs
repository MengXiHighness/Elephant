﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DS.AFP.Common.Core {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DS.AFP.Common.Core.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   使用此强类型资源类，为所有资源查找
        ///   重写当前线程的 CurrentUICulture 属性。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 Object type must be&apos;{0}&apos;to adapt to the current adaptation range. 的本地化字符串。
        /// </summary>
        public static string AdapterInvalidTypeException {
            get {
                return ResourceManager.GetString("AdapterInvalidTypeException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Plug in service configuration is incorrect. 的本地化字符串。
        /// </summary>
        public static string BindingNotFoundException {
            get {
                return ResourceManager.GetString("BindingNotFoundException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Pre load program has been completed. 的本地化字符串。
        /// </summary>
        public static string BootstrapperRunCompleted {
            get {
                return ResourceManager.GetString("BootstrapperRunCompleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The current regional name is&apos; {0}&apos;, which can&apos;t be changed again. 的本地化字符串。
        /// </summary>
        public static string CannotChangeRegionNameException {
            get {
                return ResourceManager.GetString("CannotChangeRegionNameException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Could not create a navigation target&apos;{0}&apos; 的本地化字符串。
        /// </summary>
        public static string CannotCreateNavigationTarget {
            get {
                return ResourceManager.GetString("CannotCreateNavigationTarget", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Could not register CompositeCommand itself. 的本地化字符串。
        /// </summary>
        public static string CannotRegisterCompositeCommandInItself {
            get {
                return ResourceManager.GetString("CannotRegisterCompositeCommandInItself", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The same command is not registered in the same CompositeCommand two times. 的本地化字符串。
        /// </summary>
        public static string CannotRegisterSameCommandTwice {
            get {
                return ResourceManager.GetString("CannotRegisterSameCommandTwice", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Type &apos;{0}&apos; does not implement the IRegionBehavior interface. 的本地化字符串。
        /// </summary>
        public static string CanOnlyAddTypesThatInheritIFromRegionBehavior {
            get {
                return ResourceManager.GetString("CanOnlyAddTypesThatInheritIFromRegionBehavior", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 ConfigurationStore can not be null 的本地化字符串。
        /// </summary>
        public static string ConfigurationStoreCannotBeNull {
            get {
                return ResourceManager.GetString("ConfigurationStoreCannotBeNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Configure the container to start running. 的本地化字符串。
        /// </summary>
        public static string ConfigureContainerBegin {
            get {
                return ResourceManager.GetString("ConfigureContainerBegin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Configuration container end run. 的本地化字符串。
        /// </summary>
        public static string ConfigureContainerEnd {
            get {
                return ResourceManager.GetString("ConfigureContainerEnd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Configure default behavior for the region to begin. 的本地化字符串。
        /// </summary>
        public static string ConfigureDefaultRegionBehaviorsBegin {
            get {
                return ResourceManager.GetString("ConfigureDefaultRegionBehaviorsBegin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The end of the behavior of the default zone. 的本地化字符串。
        /// </summary>
        public static string ConfigureDefaultRegionBehaviorsEnd {
            get {
                return ResourceManager.GetString("ConfigureDefaultRegionBehaviorsEnd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Configure adapter map area to start. 的本地化字符串。
        /// </summary>
        public static string ConfigureRegionAdapterMappingsBegin {
            get {
                return ResourceManager.GetString("ConfigureRegionAdapterMappingsBegin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Configuration adapter map area end. 的本地化字符串。
        /// </summary>
        public static string ConfigureRegionAdapterMappingsEnd {
            get {
                return ResourceManager.GetString("ConfigureRegionAdapterMappingsEnd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Configuration module directory. 的本地化字符串。
        /// </summary>
        public static string ConfiguringModuleCatalogBegin {
            get {
                return ResourceManager.GetString("ConfiguringModuleCatalogBegin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Configuration module directory end. 的本地化字符串。
        /// </summary>
        public static string ConfiguringModuleCatalogEnd {
            get {
                return ResourceManager.GetString("ConfiguringModuleCatalogEnd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The allocation of the singleton service loader to start. 的本地化字符串。
        /// </summary>
        public static string ConfiguringServiceLocatorSingletonBegin {
            get {
                return ResourceManager.GetString("ConfiguringServiceLocatorSingletonBegin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The allocation of the singleton service end loader. 的本地化字符串。
        /// </summary>
        public static string ConfiguringServiceLocatorSingletonEnd {
            get {
                return ResourceManager.GetString("ConfiguringServiceLocatorSingletonEnd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Configure Spring container to start. 的本地化字符串。
        /// </summary>
        public static string ConfiguringSpringContainerBegin {
            get {
                return ResourceManager.GetString("ConfiguringSpringContainerBegin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Configure Spring container end. 的本地化字符串。
        /// </summary>
        public static string ConfiguringSpringContainerEnd {
            get {
                return ResourceManager.GetString("ConfiguringSpringContainerEnd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Container initialization exception. 的本地化字符串。
        /// </summary>
        public static string ContainerInitializeException {
            get {
                return ResourceManager.GetString("ContainerInitializeException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The content in ContentControl cannot be empty.
        ///This control is associated with a region, but it has been bound to something else. If you do not explicitly set the contents of the control attribute, this may cause regionmanager inherited the attached property value changes. 的本地化字符串。
        /// </summary>
        public static string ContentControlHasContentException {
            get {
                return ResourceManager.GetString("ContentControlHasContentException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Create the module directory to start. 的本地化字符串。
        /// </summary>
        public static string CreatingModuleCatalogBegin {
            get {
                return ResourceManager.GetString("CreatingModuleCatalogBegin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Create module directory end. 的本地化字符串。
        /// </summary>
        public static string CreatingModuleCatalogEnd {
            get {
                return ResourceManager.GetString("CreatingModuleCatalogEnd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Create the Spring container to start. 的本地化字符串。
        /// </summary>
        public static string CreatingSpringContainerBegin {
            get {
                return ResourceManager.GetString("CreatingSpringContainerBegin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Create Spring container end. 的本地化字符串。
        /// </summary>
        public static string CreatingSpringContainerEnd {
            get {
                return ResourceManager.GetString("CreatingSpringContainerEnd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The module directory has been found to have at least one cycle dependent. Module cycle dependence is prohibited.
        /// 的本地化字符串。
        /// </summary>
        public static string CyclicDependencyFound {
            get {
                return ResourceManager.GetString("CyclicDependencyFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Stop activities in this type of area. 的本地化字符串。
        /// </summary>
        public static string DeactiveNotPossibleException {
            get {
                return ResourceManager.GetString("DeactiveNotPossibleException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 {1}: {2}. Priority: {3}. Time stamp:{0:u}. 的本地化字符串。
        /// </summary>
        public static string DefaultTextLoggerPattern {
            get {
                return ResourceManager.GetString("DefaultTextLoggerPattern", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 DeactiveNotPossibleException 的本地化字符串。
        /// </summary>
        public static string DelegateCommandDelegatesCannDeactiveNotPossibleException {
            get {
                return ResourceManager.GetString("DelegateCommandDelegatesCannDeactiveNotPossibleException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 ExecuteMethod and canExecuteMethod can not be null. 的本地化字符串。
        /// </summary>
        public static string DelegateCommandDelegatesCannotBeNull {
            get {
                return ResourceManager.GetString("DelegateCommandDelegatesCannotBeNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似  Generic of type DelegateCommand &lt;T&gt; is neither an object nor a nullable type. 的本地化字符串。
        /// </summary>
        public static string DelegateCommandInvalidGenericPayloadType {
            get {
                return ResourceManager.GetString("DelegateCommandInvalidGenericPayloadType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Could not add the dependency relationship of the unknown module {0} 的本地化字符串。
        /// </summary>
        public static string DependencyForUnknownModule {
            get {
                return ResourceManager.GetString("DependencyForUnknownModule", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The definition of a module depends on another module that does not define the load.: {0} 的本地化字符串。
        /// </summary>
        public static string DependencyOnMissingModule {
            get {
                return ResourceManager.GetString("DependencyOnMissingModule", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Not found directory{0} 的本地化字符串。
        /// </summary>
        public static string DirectoryNotFound {
            get {
                return ResourceManager.GetString("DirectoryNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The loader has found the name replication module for {0} 的本地化字符串。
        /// </summary>
        public static string DuplicatedModule {
            get {
                return ResourceManager.GetString("DuplicatedModule", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The loader has found a group of {0} replication module 的本地化字符串。
        /// </summary>
        public static string DuplicatedModuleGroup {
            get {
                return ResourceManager.GetString("DuplicatedModuleGroup", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Cannot retrieve module type {0} from the loaded assembly. You may need to specify a fully qualified type name 的本地化字符串。
        /// </summary>
        public static string FailedToGetType {
            get {
                return ResourceManager.GetString("FailedToGetType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 An exception occurred during the initialization module {0}.
        ///     - Exception message：{2}
        ///    - The module tries to load the assembly：{1}
        ///  Check for more information on the exception of the InnerException property. If an exception occurs in the process of creating an DI container, you can call.GetRootException () method to help find the root cause of the problem.
        ///   的本地化字符串。
        /// </summary>
        public static string FailedToLoadModule {
            get {
                return ResourceManager.GetString("FailedToLoadModule", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 An exception occurred during the initialization module {0}.
        ///     - Exception message：{1}
        /// Check for more information on the exception of the InnerException property. If an exception occurs in the process of creating an DI container, you can call.GetRootException () method to help find the root cause of the problem. 的本地化字符串。
        /// </summary>
        public static string FailedToLoadModuleNoAssemblyInfo {
            get {
                return ResourceManager.GetString("FailedToLoadModuleNoAssemblyInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Unable to load module type{0}。
        ///
        ///If this error occurs when using MEF in the Silverlight application, make sure that the CopyLocal property of the MefExtensions assembly of the master program is True, while the other assemblies are False.
        ///
        ///Error is：{1} 的本地化字符串。
        /// </summary>
        public static string FailedToRetrieveModule {
            get {
                return ResourceManager.GetString("FailedToRetrieveModule", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Run time HostControl can not be null value. 的本地化字符串。
        /// </summary>
        public static string HostControlCannotBeNull {
            get {
                return ResourceManager.GetString("HostControlCannotBeNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 After the Attach method is called, the HostControl property cannot be set. 的本地化字符串。
        /// </summary>
        public static string HostControlCannotBeSetAfterAttach {
            get {
                return ResourceManager.GetString("HostControlCannotBeSetAfterAttach", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 HostControl type must be a TabControl. 的本地化字符串。
        /// </summary>
        public static string HostControlMustBeATabControl {
            get {
                return ResourceManager.GetString("HostControlMustBeATabControl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The IModuleEnumerator interface is obsolete and no longer be used. Please use ModuleCatalog instead. 的本地化字符串。
        /// </summary>
        public static string IEnumeratorObsolete {
            get {
                return ResourceManager.GetString("IEnumeratorObsolete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Initialization module. 的本地化字符串。
        /// </summary>
        public static string InitializingModulesBegin {
            get {
                return ResourceManager.GetString("InitializingModulesBegin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Initialization module end. 的本地化字符串。
        /// </summary>
        public static string InitializingModulesEnd {
            get {
                return ResourceManager.GetString("InitializingModulesEnd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Parameters must be an effective absolute URI for a program set file 的本地化字符串。
        /// </summary>
        public static string InvalidArgumentAssemblyUri {
            get {
                return ResourceManager.GetString("InvalidArgumentAssemblyUri", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The goal to achieve the IDelegateReference interface should be type {0}. 的本地化字符串。
        /// </summary>
        public static string InvalidDelegateRerefenceTypeException {
            get {
                return ResourceManager.GetString("InvalidDelegateRerefenceTypeException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 ItemsSource&apos;s ItemsControl property is not available.
        ///  This control is associated with a region, but it has been bound to something else. If you do not explicitly set control ItemSource attribute and of the anomalies may cause regionmanager inherited the attached property value changes. 的本地化字符串。
        /// </summary>
        public static string ItemsControlHasItemsSourceException {
            get {
                return ResourceManager.GetString("ItemsControlHasItemsSourceException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Log creation success. 的本地化字符串。
        /// </summary>
        public static string LoggerCreatedSuccessfully {
            get {
                return ResourceManager.GetString("LoggerCreatedSuccessfully", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Given mapping type: {0}, already registered. 的本地化字符串。
        /// </summary>
        public static string MappingExistsException {
            get {
                return ResourceManager.GetString("MappingExistsException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Module {0} depends on other modules that are not in the same group. 的本地化字符串。
        /// </summary>
        public static string ModuleDependenciesNotMetInGroup {
            get {
                return ResourceManager.GetString("ModuleDependenciesNotMetInGroup", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Not found in the same directory{0} 的本地化字符串。
        /// </summary>
        public static string ModuleNotFound {
            get {
                return ResourceManager.GetString("ModuleNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Module path cannot be null or empty. 的本地化字符串。
        /// </summary>
        public static string ModulePathCannotBeNullOrEmpty {
            get {
                return ResourceManager.GetString("ModulePathCannotBeNullOrEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Cannot load {0} type in assembly {1}. 的本地化字符串。
        /// </summary>
        public static string ModuleTypeNotFound {
            get {
                return ResourceManager.GetString("ModuleTypeNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Navigation already exists in the process area, the name is‘{0} 的本地化字符串。
        /// </summary>
        public static string NavigationInProgress {
            get {
                return ResourceManager.GetString("NavigationInProgress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Navigation cannot continue until the area is set to RegionNavigationService 的本地化字符串。
        /// </summary>
        public static string NavigationServiceHasNoRegion {
            get {
                return ResourceManager.GetString("NavigationServiceHasNoRegion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The type IRegionAdapter {0} interface is not registered with the region adaptation map. In the pre load program, you can register the IRegionAdapter interface by overriding the ConfigureRegionAdapterMappings method. 的本地化字符串。
        /// </summary>
        public static string NoRegionAdapterException {
            get {
                return ResourceManager.GetString("NoRegionAdapterException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The module type loader retrieves the specified module can not present in the module manager. 的本地化字符串。
        /// </summary>
        public static string NoRetrieverCanRetrieveModule {
            get {
                return ResourceManager.GetString("NoRetrieverCanRetrieveModule", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Create log exception. 的本地化字符串。
        /// </summary>
        public static string NullLoggerFacadeException {
            get {
                return ResourceManager.GetString("NullLoggerFacadeException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 ModuleCatalog is an empty exception. 的本地化字符串。
        /// </summary>
        public static string NullModuleCatalogException {
            get {
                return ResourceManager.GetString("NullModuleCatalogException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The same container is null exception. 的本地化字符串。
        /// </summary>
        public static string NullUnityContainerException {
            get {
                return ResourceManager.GetString("NullUnityContainerException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The view is added to the area &apos;{0}&apos; when an exception occurs.
        ///    -  Most likely to cause abnormal：“{1}”。
        ///    You can also check for more information on the exception&apos;s InnerException property, or call.GetRootException () method 的本地化字符串。
        /// </summary>
        public static string OnViewRegisteredException {
            get {
                return ResourceManager.GetString("OnViewRegisteredException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Pre initialization module. 的本地化字符串。
        /// </summary>
        public static string PreInitializeModulesBegin {
            get {
                return ResourceManager.GetString("PreInitializeModulesBegin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 End of pre initialization module. 的本地化字符串。
        /// </summary>
        public static string PreInitializeModulesEnd {
            get {
                return ResourceManager.GetString("PreInitializeModulesEnd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Member access expression does not support property access. 的本地化字符串。
        /// </summary>
        public static string PropertySupport_ExpressionNotProperty_Exception {
            get {
                return ResourceManager.GetString("PropertySupport_ExpressionNotProperty_Exception", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Expression is not a member access expression. 的本地化字符串。
        /// </summary>
        public static string PropertySupport_NotMemberAccessExpression_Exception {
            get {
                return ResourceManager.GetString("PropertySupport_NotMemberAccessExpression_Exception", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Reference property is a static property. 的本地化字符串。
        /// </summary>
        public static string PropertySupport_StaticExpression_Exception {
            get {
                return ResourceManager.GetString("PropertySupport_StaticExpression_Exception", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 When the Region property is empty, the Attach method cannot be invoked. 的本地化字符串。
        /// </summary>
        public static string RegionBehaviorAttachCannotBeCallWithNullRegion {
            get {
                return ResourceManager.GetString("RegionBehaviorAttachCannotBeCallWithNullRegion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 After the Attach method is called, the Region property cannot be set. 的本地化字符串。
        /// </summary>
        public static string RegionBehaviorRegionCannotBeSetAfterAttach {
            get {
                return ResourceManager.GetString("RegionBehaviorRegionCannotBeSetAfterAttach", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Create an exception for a zone name&apos;{0}. The only exceptions are：{1}。 的本地化字符串。
        /// </summary>
        public static string RegionCreationException {
            get {
                return ResourceManager.GetString("RegionCreationException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 This area has already added a name to &quot;{0}&quot;, and cannot be added to a different name (&apos;{1}&apos;) in the area management. 的本地化字符串。
        /// </summary>
        public static string RegionManagerWithDifferentNameException {
            get {
                return ResourceManager.GetString("RegionManagerWithDifferentNameException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The area name cannot be null or empty. 的本地化字符串。
        /// </summary>
        public static string RegionNameCannotBeEmptyException {
            get {
                return ResourceManager.GetString("RegionNameCannotBeEmptyException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Given the name of the region has been registered: {0} 的本地化字符串。
        /// </summary>
        public static string RegionNameExistsException {
            get {
                return ResourceManager.GetString("RegionNameExistsException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 RegionManager does not contain a region named {0}. 的本地化字符串。
        /// </summary>
        public static string RegionNotFound {
            get {
                return ResourceManager.GetString("RegionNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 RegionManager does not include the {0} region 的本地化字符串。
        /// </summary>
        public static string RegionNotInRegionManagerException {
            get {
                return ResourceManager.GetString("RegionNotInRegionManagerException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Existing views in the area 的本地化字符串。
        /// </summary>
        public static string RegionViewExistsException {
            get {
                return ResourceManager.GetString("RegionViewExistsException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The view name already exists in the area’{0}‘ 的本地化字符串。
        /// </summary>
        public static string RegionViewNameExistsException {
            get {
                return ResourceManager.GetString("RegionViewNameExistsException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Register frame exception type start. 的本地化字符串。
        /// </summary>
        public static string RegisteringFrameworkExceptionTypesBegin {
            get {
                return ResourceManager.GetString("RegisteringFrameworkExceptionTypesBegin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The end of the abnormal type of registration framework. 的本地化字符串。
        /// </summary>
        public static string RegisteringFrameworkExceptionTypesEnd {
            get {
                return ResourceManager.GetString("RegisteringFrameworkExceptionTypesEnd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Failed to find the {0} service contract interface type. 的本地化字符串。
        /// </summary>
        public static string ServiceContractNotFoundException {
            get {
                return ResourceManager.GetString("ServiceContractNotFoundException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 When the application starts, the tag module is automatically initialized with the {0}, but the module depends on some modules that are required to initialize the tag. To resolve this error, set up the module property InitializationMode=WhenAvailable, or delete the validation extension method in the ModuleCatalog class.
        /// 的本地化字符串。
        /// </summary>
        public static string StartupModuleDependsOnAnOnDemandModule {
            get {
                return ResourceManager.GetString("StartupModuleDependsOnAnOnDemandModule", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The supplied string parameter {0} cannot be null or null 的本地化字符串。
        /// </summary>
        public static string StringCannotBeNullOrEmpty {
            get {
                return ResourceManager.GetString("StringCannotBeNullOrEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The primary key is&apos; {0} &apos;BehaviorType 的本地化字符串。
        /// </summary>
        public static string TypeWithKeyNotRegistered {
            get {
                return ResourceManager.GetString("TypeWithKeyNotRegistered", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 A exception has occurred while trying to create a regional object.
        ///-The most likely exception is:：&apos;{0}“。
        ///For more information, please check InnerExceptions, or call.GetRootException. 的本地化字符串。
        /// </summary>
        public static string UpdateRegionException {
            get {
                return ResourceManager.GetString("UpdateRegionException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The value must be ModuleInfo type. 的本地化字符串。
        /// </summary>
        public static string ValueMustBeOfTypeModuleInfo {
            get {
                return ResourceManager.GetString("ValueMustBeOfTypeModuleInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Not found{0} 的本地化字符串。
        /// </summary>
        public static string ValueNotFound {
            get {
                return ResourceManager.GetString("ValueNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The area does not contain the specified view 的本地化字符串。
        /// </summary>
        public static string ViewNotInRegionException {
            get {
                return ResourceManager.GetString("ViewNotInRegionException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 WCF host anomaly. 的本地化字符串。
        /// </summary>
        public static string WCFHostException {
            get {
                return ResourceManager.GetString("WCFHostException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Create WCF service exception. 的本地化字符串。
        /// </summary>
        public static string WCFServiceCreateException {
            get {
                return ResourceManager.GetString("WCFServiceCreateException", resourceCulture);
            }
        }
    }
}
