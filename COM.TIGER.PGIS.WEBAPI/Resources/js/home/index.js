/// <reference path="../common.js" />
/// <reference path="../extjs4.2/ext-all.js" />

Ext.onReady(function () {
    viewport();
});

function viewport() {
    ///<summary>
    /// 获取布局页面
    ///</summary>
    ///<return>返回Ext.layout.ViewPort类型的一个实例</return>

    function getMenu() {
        ///<summary>
        /// 获取菜单信息
        ///</summary>
        ///<return>Array的一个实例</return>

        var r = [];
        var c = getConfiguration();
        r.push(c);
        var p = getApi();
        r.push(p);
        var n = getAnothers();
        r.push(n);

        return r;
    }

    function getParamTree() {
        ///<summary>
        /// 参数项的tree
        ///</summary>
        ///<return>返回Ext.tree.TreePanel类型的一个实例</return>

        var s = new Ext.data.TreeStore({
            root: {
                text: 'root',
                children: [
                    {
                        id: 'first leaf 1',
                        text: 'first leaf 1',
                        children:[
                            {
                                id: 'second leaf 1',
                                text: 'second leaf 1',
                                leaf:true
                            }
                        ]
                    },
                    {
                        id: 'first leaf 2',
                        text: 'first leaf 2',
                        children: [
                            {
                                id: 'second leaf 2',
                                text: 'second leaf 2',
                                leaf:true
                            }
                        ]
                    }
                ]
            }
        });

        function befordrop(n, o, om, p, h) {
            if (om.get('leaf')) {
                om.set('leaf', false);
                om.set('loaded', true);
            }
            return true;
        }

        var t = new Ext.tree.TreePanel({
            viewConfig: {
                plugins: {ptype:'treeviewdragdrop'}
            },
            store: s
        });
        //展开所有的节点
        t.getRootNode().expand();
        t.on("itemclick", function (n, o, i) {
            Ext.Msg.show({
                title: '',
                msg: o.id,
                animateTarget: i
            });
            //Ext.Msg.alert("", o.data.text);
        });
        t.view.on("beforedrop", befordrop);
        return t;
    }

    function getExampleTree() {
        ///<summary>
        /// 测试用到的tree
        ///</summary>
        ///<return>返回Ext.tree.TreePanel类型的一个测试实例</return>
        var s1 = new Ext.data.TreeStore({
            root: {
                text: '这是一个测试的Tree',
                children: [
                    {
                        id: 'first leaf 1',
                        text: '这是父节点1',
                        children: [
                            {
                                id: 'second leaf 1',
                                text: '这是子节点1',
                                leaf: true
                            },
                            {
                                id: 'second leaf 2',
                                text: '这是子节点2',
                                leaf: true
                            },
                            {
                                id: 'second leaf 3',
                                text: '这是子节点3',
                                leaf: true
                            }
                        ]
                    },
                    {
                        id: 'first leaf 2',
                        text: '这是父节点2',
                        children: [
                            {
                                id: 'second leaf 4',
                                text: '这是子节点4',
                                leaf: true
                            }
                        ]
                    }
                ]
            }
        });

        var t1 = new Ext.tree.TreePanel({
            viewConfig: {
                plugins: { ptype: 'treeviewdragdrop' }
            },
            store: s1
        });

        return t1;
    }

    function getConfiguration() {
        ///<summary>
        /// 获取模块配置菜单信息
        ///</summary>
        ///<return>object的的一个实例</return>

        var o = {
            title: 'modules configuration',
            layout: {
                type: 'vbox',
                padding: 5,
                align: 'stretch'
            },
            autoScroll: true,
            defaults: {
                margins: '0 0 5 0'
            },
            items: [
                {
                    xtype: 'button',
                    text: '<div class="menu-margin">modules params</div>',
                    handler: function () {
                        var c = Ext.getCmp('body_center');
                        c.removeAll();
                        c.add([
                            {
                                region: 'west',
                                width: 300,
                                layout: 'border',
                                split: true,
                                border:false,
                                items: [
                                    {
                                        region: 'north',
                                        height: 26,
                                        xtype: 'toolbar',
                                        items: [
                                            {
                                                xtype: 'button',
                                                text: '+',
                                                handler: function () {
                                                    Ext.Msg.alert("", "add a top param item");
                                                }
                                            },
                                            '->',
                                            {
                                                xtype: 'button',
                                                text: 'Expand',
                                                handler: function () {
                                                    Ext.Msg.alert("", "Expand all top param items");
                                                }
                                            },
                                            '-',
                                            {
                                                xtype: 'button',
                                                text: 'Unexpand',
                                                handler: function () {
                                                    Ext.Msg.alert("", "Unexpand all top param items");
                                                }
                                            }
                                        ]
                                    },
                                    {
                                        region: 'center',
                                        layout: 'fit',
                                        border: false,
                                        items: [
                                            getParamTree()
                                        ]
                                    }
                                ]
                            },
                            {
                                region: 'center',
                                layout: 'fit',
                                items: [
                                    {
                                        html: 'description'
                                    }
                                ]
                            }
                        ]);
                    }
                },
                {
                    xtype: 'button',
                    text: '<div class="menu-margin">modules area</div>',
                    handler: function () {
                        var c = Ext.getCmp('body_center');
                        c.removeAll();
                        Ext.Msg.alert("", "area configuration");
                    }
                },
                {
                    xtype: 'button',
                    text: '<div class="menu-margin">这是测试样例菜单1</div>',
                    handler: function () {
                        var c = Ext.getCmp('body_center');
                        c.removeAll();
                        c.add([
                            {
                                region: 'west',
                                width: 300,
                                layout: 'border',
                                split: true,
                                border: false,
                                items: [
                                    {
                                        region: 'north',
                                        title: '测试样例二级菜单',
                                        layout: {
                                            type: 'vbox',
                                            padding: 5,
                                            align: 'stretch'
                                        },
                                        autoScroll: true,
                                        defaults: {
                                            margins: '0 0 5 0'
                                        },
                                        items: [
                                            {
                                                xtype: 'button',
                                                text: '<div class="menu-margin">按钮1</div>',
                                                handler: function () {
                                                    var c = Ext.getCmp('body_center');                                                   
                                                    Ext.Msg.alert("", "这是按钮1！！");
                                                }
                                            },
                                            {
                                                xtype: 'button',
                                                text: '<div class="menu-margin">按钮2</div>',
                                                handler: function () {
                                                    var c = Ext.getCmp('body_center');
                                                    Ext.Msg.alert("", "这是按钮2！！");
                                                }
                                            },
                                            {
                                                xtype: 'panel',
                                                title: '二级菜单容器',
                                                layout: {
                                                    type: 'vbox',
                                                    padding: 5,
                                                    align: 'stretch'
                                                },
                                                collapsible: true,
                                                defaults: {
                                                    margins: '0 0 5 0'
                                                },
                                                items: [
                                                    {
                                                        xtype: 'button',
                                                        text: '<div class="menu-margin">容器中的按钮一</div>',
                                                        handler: function () {
                                                            Ext.Msg.alert("", "这是容器中的按钮一！");
                                                        }
                                                    },
                                                    {
                                                        xtype: 'button',
                                                        text: '<div class="menu-margin">容器中的按钮二</div>',
                                                        handler: function () {
                                                            Ext.Msg.alert("", "容器中的按钮二！");
                                                        }
                                                    },
                                                ]
                                            }
                                        ]
                                    },
                                    {
                                        region: 'center',
                                        layout: 'fit',
                                        border: false,
                                        items: [                                            
                                            {
                                                html: '这里是将要显示的文本内容！'
                                            }
                                        ]
                                    }
                                ]
                            },
                            {
                                region: 'center',
                                layout: 'fit',
                                items: [
                                    getExampleTree()
                                ]
                            }
                        ]);
                    }
                },
                {
                    xtype: 'panel',
                    title: 'Authentize Control',
                    layout: {
                        type: 'vbox',
                        padding: 5,
                        align: 'stretch'
                    },
                    collapsible: true,
                    defaults: {
                        margins: '0 0 5 0'
                    },
                    items: [
                        {
                            xtype: 'button',
                            text: '<div class="menu-margin">modules menus</div>',
                            handler: function () {
                                Ext.Msg.alert("", "menus configuration");
                            }
                        },
                        {
                            xtype: 'button',
                            text: '<div class="menu-margin">modules roles</div>',
                            handler: function () {
                                Ext.Msg.alert("", "roles configuration");
                            }
                        },
                        {
                            xtype: 'button',
                            text: '<div class="menu-margin">modules department</div>',
                            handler: function () {
                                Ext.Msg.alert("", "departments configuration");
                            }
                        },
                        {
                            xtype: 'button',
                            text: '<div class="menu-margin">modules users</div>',
                            handler: function () {
                                Ext.Msg.alert("", "users configuration");
                            }
                        },
                    ]
                }
            ]
        };

        return o;
    }

    function getApi() {
        ///<summary>
        /// 获取模块接口说明菜单信息
        ///</summary>
        ///<return>object的的一个实例</return>

        var o = {
            title: 'modules api'
        };

        return o;
    }

    function getAnothers() {
        ///<summary>
        /// 获取其他菜单信息
        ///</summary>
        ///<return>object的的一个实例</return>

        var o = {
            title: 'anothers'
        };

        return o;
    }

    /* =============================================================
    ## 调用Ext框架布局
    ==============================================================*/
    var menus = getMenu();
    var viewport = new Ext.Viewport({
        layout: 'border',
        items: [
            {
                region: 'north',
                id: 'body_header',
                xtype: 'toolbar',
                items: [
                    '<span style="font-size:16px; font-weight:700;">Application Configuration System<span>',
                    '->',
                    {
                        text: 'Login',
                        iconCls: 'icon-login',
                        handler: function () { alert("login"); }
                    },
                    '-',
                    {
                        text: 'Password',
                        iconCls: 'icon-password',
                        handler: function () { alert("password"); }
                    }
                ]
            },
            {
                region: 'west',
                id: 'body_left',
                width: 200,
                split: true,
                layout: 'accordion',
                layoutConfig: {
                    titleCollapse: true,
                    animate: true,
                    activeOnTop: false
                },
                //菜单信息，后台获取
                items: menus
            },
            {
                xtype: 'container',
                region: 'center',
                id: 'body_center',
                autoScroll: true,
                layout: 'border',
                items: []
            },
            {
                region: 'south',
                id: 'body_footer',
                height: 20,
                html: '<center style="font-size:12px;">版权所有：杭州塔格科技有限公司 &copy 2014</center>'
            }
        ]
    });

    return viewport;
}

