using System.Web;
using System.Web.Optimization;

namespace Basico.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/vendors/modernizr.js"));

            bundles.Add(new ScriptBundle("~/bundles/vendors").Include(
                "~/Scripts/vendors/jquery.js",
                //"~/Scripts/vendors/jquery.signalR-0.5.0.js",
                "~/Scripts/vendors/jquery.signalR-2.2.1.js",

                "~/Scripts/theme/bootstrap.js",
                "~/Scripts/vendors/toastr.js",                
                "~/Scripts/vendors/respond.src.js",
                "~/Scripts/vendors/angular.js",
                "~/Scripts/vendors/angular-animate.js",

                "~/Scripts/theme/angular-aria.js",
                "~/Scripts/theme/angular-material.js",
                "~/Scripts/vendors/angular-messages.min.js", //

                "~/Scripts/vendors/angular-route.js",
                "~/Scripts/vendors/angular-cookies.js",
                "~/Scripts/vendors/angular-validator.js",
                "~/Scripts/vendors/angular-base64.js",
                "~/Scripts/vendors/angular-file-upload.js",
                "~/Scripts/vendors/angucomplete-alt.min.js",
                "~/Scripts/vendors/ui-bootstrap-tpls-2.2.0.js",                
                "~/Scripts/vendors/underscore.js",



                "~/Scripts/vendors/jquery.fancybox.js",
                "~/Scripts/vendors/jquery.fancybox-media.js",                
                "~/Scripts/vendors/moment.js",                                
                "~/Scripts/vendors/moment-timezone-with-data.js",
                "~/Scripts/vendors/angular-moment.min.js",
                "~/Scripts/vendors/loading-bar.js",                
                "~/Scripts/vendors/angular-password.js",
                "~/Scripts/vendors/favico.min.js",
                "~/Scripts/vendors/mask.js",
                "~/Scripts/theme/grayscale.js",
                "~/Scripts/theme/jquery.easing.min.js",                
                "~/Scripts/vendors/ngDroplet.js",
                "~/Scripts/vendors/jquery.mCustomScrollbar.concat.min.js",
                "~/Scripts/vendors/angular-cache-buster.js"



                //"~/Scripts/vendors/jquery.raty.js", //Star Rating
                //"~/Scripts/vendors/morris.js", //Charts
                //"~/Scripts/vendors/raphael.js", //Charts Vector

                ));

            //"~/Scripts/spa/directives/rating.directive.js",
            //"~/Scripts/spa/directives/availableMovie.directive.js",
            
            bundles.Add(new ScriptBundle("~/bundles/spa").Include(
                "~/Scripts/spa/_modules/common.core.js",
                "~/Scripts/spa/_modules/common.ui.js",
                "~/Scripts/spa/app.js",

                "~/Scripts/spa/home/rootCtrl.js",
                "~/Scripts/spa/home/indexCtrl.js",

                "~/Scripts/spa/_services/apiService.js",
                "~/Scripts/spa/_services/notificationService.js",
                "~/Scripts/spa/_services/membershipService.js",
                "~/Scripts/spa/_services/fileUploadService.js",
                "~/Scripts/spa/_services/favicoService.js",


                "~/Scripts/spa/_layout/topBar.directive.js",
                "~/Scripts/spa/_layout/sideBar.directive.js",
                "~/Scripts/spa/_layout/customPager.directive.js",

                "~/Scripts/spa/_directives/validate-cnpj.directive.js",
                "~/Scripts/spa/_directives/validate-cpf.directive.js",
                "~/Scripts/spa/_directives/xlsx-model.js",
                "~/Scripts/spa/_directives/app_filters.js",
                "~/Scripts/spa/_directives/table-myisaki.directive.js",
                

                "~/Scripts/spa/account/loginCtrl.js",
                "~/Scripts/spa/account/loginModalCtrl.js",
                "~/Scripts/spa/account/registerCtrl.js",
                "~/Scripts/spa/account/registerModalCtrl.js",
                "~/Scripts/spa/account/registerModal.directive.js",
                "~/Scripts/spa/account/editAccountCtrl.js",
                "~/Scripts/spa/account/loginTokenCtrl.js"
                //"~/Scripts/spa/_services/_hubs.js"

                , "~/Scripts/spa/chat/*.js"

                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(

                "~/content/theme/bootstrap.css",
                "~/content/theme/grayscale.css",
                "~/content/theme/angular-material.css",

                //"~/content/css/morris.css", //Chart CSS + JS
                "~/content/css/font-awesome.css",               
                "~/content/css/toastr.css",
                "~/content/css/jquery.fancybox.css",
                "~/content/css/loading-bar.css",
                "~/content/css/site.css",
                "~/content/css/chatMain.css",
                "~/content/css/jquery.mCustomScrollbar.min.css"

                ));

            BundleTable.EnableOptimizations = false;

        }
    }
}
