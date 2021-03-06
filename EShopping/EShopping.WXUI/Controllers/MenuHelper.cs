﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EShopping.WXUI.Controllers
{
    public static class MenuHelper
    {

        public static MvcHtmlString ValidationSummaryAlert(this HtmlHelper helper)
        {
            if (helper.ViewData.ModelState.IsValid)
                return MvcHtmlString.Empty;

            StringBuilder sb = new StringBuilder();
            string html = CreateErrorDiv();
            sb.Append(html);
            string errorStr = "";
            helper.ViewData.ModelState.Select(x => x.Value.Errors).ToList().ForEach(collection =>
            {
                foreach (ModelError error in collection)
                {
                    errorStr += error.ErrorMessage + "<br/>";
                }
            });

            return MvcHtmlString.Create(sb.ToString().Replace("$error$",errorStr));
        }



        public static string CreateErrorDiv()
        {
            string errorTmp = @"<style type='text/css'>
                                * {
                                    margin: 0px;
                                    padding: 0px;
                                }

                                a, a:hover {
                                    color: #333;
                                    text-decoration: none;
                                }

                                #errLayer {
                                    position: fixed;
                                    top: 0px;
                                    left: 0px;
                                    z-index: 9999;
                                    width: 100%;
                                    height: 100%;
                                    background: rgba(100, 100, 100, 0.5);
                                }

                                .layer_bg {
                                    position: absolute;
                                    left: 10%;
                                    top: 50%;
                                    width: 80%;
                                    background: #fff;
                                    border-radius: 10px;
                                    -webkit-border-radius: 10px;
                                    -moz-border-radius: 10px;
                                    overflow: hidden;
                                }

                                .layer_header, .layer_footer {
                                    position: absolute;
                                    left: 0px;
                                    width: 100%;
                                    height: 40px;
                                    text-align: center;
                                    font-family: 'microsoft yahei';
                                }

                                .layer_header {
                                    top: 0px;
                                }

                                    .layer_header a#layerCloser {
                                        float: right;
                                        display: block;
                                        width: 40px;
                                        line-height: 40px;
                                    }

                                .layer_footer {
                                    bottom: 0px;
                                    background: #333333;
                                }

                                    .layer_footer a {
                                        color: #fff;
                                        line-height: 40px;
                                    }

                                .layer_content {
                                    padding: 40px 10px;
                                }

                                .layer_inner {
                                    padding: 10px 0px;
                                }

                                    .layer_inner, .layer_inner p {
                                        text-align: center;
                                    }
                            </style>

                                <div id='errLayer'>
                                    <div class='layer_bg'>
                                        <div class='layer_header'>
                                            <a id='layerCloser' href='javascript:;'>X</a>
                                        </div>
                                        <div class='layer_content'>
                                            <div class='layer_inner'>
                                                <p>$error$</p>
                                            </div>
                                        </div>
                                        <div class='layer_footer'>
                                            <a id='footBtn' href='javascript:;' title=''>关闭</a>
                                        </div>
                                    </div>
                                </div>
                                <script type='text/javascript'>
                                    $(function () {
                                        $('#layerCloser, #footBtn').on('click', function () {
                                            $('#errLayer').hide();
                                        });
                                        $(document).on('click', function () {
                                            $('#errLayer').hide();
                                        });
                                        $('.layer_bg').on('click', function (ev) {
                                            ev.stopPropagation();
                                            ev.preventDefault();
                                            return false;
                                        })
                                    });
                                </script> ";

            return errorTmp;
        }
    }
}