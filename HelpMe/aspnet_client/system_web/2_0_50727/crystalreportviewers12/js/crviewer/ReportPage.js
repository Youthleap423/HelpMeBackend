/* Copyright (c) Business Objects 2006. All rights reserved. */

if (typeof(bobj.crv.ReportPage) == 'undefined') {
    bobj.crv.ReportPage = {};
}

/**
 * ReportPage constructor
 *
 * @param kwArgs.id        [String]  DOM node id
 * @param kwArgs.contentId [String]  DOM node id of report page content container
 * @param kwArgs.bgColor   [String]  Background color of the page
 * @param kwArgs.width        [Int] Page content's width in pixels
 * @param kwArgs.height       [Int] Page content's height in pixels
 * @param kwArgs.topMargin    [Int] Top margin of report page in pixels
 * @param kwArgs.rightMargin  [Int] Right margin of report page in pixels
 * @param kwArgs.bottomMargin [Int] Bottom margin of report page in pixels
 * @param kwArgs.leftMargin   [Int] Left margin of report page in pixels
 */
 
bobj.crv.ReportPage.DocumentView = {
    WEB_LAYOUT : 'weblayout',
    PRINT_LAYOUT: 'printlayout'
};

bobj.crv.newReportPage = function(kwArgs) {
    kwArgs = MochiKit.Base.update({
        id: bobj.uniqueId(),
        bgColor: '#FFFFFF',
        width: 720,
        height: 984,
        topMargin: 0,
        rightMargin: 0,
        leftMargin: 0,
        bottomMargin: 0,
        documentView: bobj.crv.ReportPage.DocumentView.PRINT_LAYOUT
    }, kwArgs);
    
    var o = newWidget(kwArgs.id);    
    o.widgetType = 'ReportPage';
    
    // Update instance with constructor arguments
    bobj.fillIn(o, kwArgs);
    
    // Update instance with member functions
    o.initOld = o.init;
    o.resizeOld = o.resize;
    MochiKit.Base.update(o, bobj.crv.ReportPage);
    
    return o;
};

/**
 * Overrides parent. Sets the content of the report page.
 *
 * @param content [String|DOM Node]  Html or Node to use as report page content 
 */
bobj.crv.ReportPage.setHTML = function (content) {
    var pageNode = this._pageNode;
    bobj.removeAllChildElements(pageNode);
    
    if (bobj.isString(content)) {
        pageNode.innerHTML = content;
    }
    else if (bobj.isObject(content)) {
        
        pageNode.appendChild(content);
        var contentStyle = content.style;
        contentStyle.display = 'block';
        contentStyle.visibility = 'visible';
    }
};

bobj.crv.ReportPage.update = function(update, updatePack) {
    if(update && update.cons == "bobj.crv.newReportPage") {
        this.updateSize(update);
        this.updateHTML(update);
        this.layer.scrollLeft = 0;
        this.layer.scrollTop =0;  
    }
};

bobj.crv.ReportPage.updateHTML = function(update) {
    var cssID = this.id + '_stylesheet';
    var prevStyle = getLayer(cssID);
    var styleText = '';
    
    if(update) {
        var html_style = bobj.html.extractStyles(update.args.content);
        for (var i = 0, c = html_style.styles.length; i < c; i++) {
            styleText += html_style.styles[i].text + '\n';
        } 

        if(prevStyle) {
            MochiKit.DOM.removeElement(prevStyle);
        }
        
        bobj.addStyleSheet(styleText, cssID);
        this.setHTML(html_style.html);
        
        var divs = this._pageNode.getElementsByTagName("div");
        if(divs && divs[0]) {
            divs[0].style.position = "relative";
            divs[0].style.visibility = "visible";
        }        
    }


}
/* Updates size of report page based on update object
 * @param update [{width,height,marginLeft,marginRight,marginTop}] dimension and margins of report p
 */
bobj.crv.ReportPage.updateSize= function(update) {
    if(update) {
        this.width = (update.args.width != undefined) ? update.args.width : this.width;
        this.height = (update.args.height != undefined) ? update.args.height : this.height;
        this.leftMargin = (update.args.leftMargin != undefined) ? update.args.leftMargin : this.leftMargin;
        this.rightMargin = (update.args.rightMargin != undefined) ? update.args.rightMargin : this.rightMargin;
        this.topMargin = (update.args.topMargin != undefined) ? update.args.topMargin : this.topMargin;
        this.bottomMargin = (update.args.bottomMargin != undefined) ? update.args.bottomMargin : this.bottomMargin;
    }
    var isBorderBoxModel = bobj.isBorderBoxModel();
    var pageOuterHeight = this.height + this.topMargin + this.bottomMargin;
    var pageOuterWidth = this.width + this.leftMargin + this.rightMargin;
    
    var contentHeight = isBorderBoxModel ? pageOuterHeight : this.height;
    var contentWidth = isBorderBoxModel ? pageOuterWidth : this.width;
    
    if(this._pageCtnNode) {
        this._pageCtnNode.style.width = pageOuterWidth + 'px';
        this._pageCtnNode.style.height = pageOuterHeight + 'px';
    }
    if(this._pageNode) {
        this._pageNode.style.width = contentWidth + 'px';
        this._pageNode.style.height = contentHeight + 'px';
        
        this._pageNode.style.paddingTop = this.topMargin + 'px';
        this._pageNode.style.paddingRight = this.rightMargin + 'px';
        this._pageNode.style.paddingLeft = this.leftMargin + 'px';
        this._pageNode.style.paddingBottom = this.bottomMargin +'px';    
    }
    
    if(this._shadowNode) {
        this._shadowNode.style.width = pageOuterWidth + 'px';
        this._shadowNode.style.height = pageOuterHeight + 'px';
    }

}

bobj.crv.ReportPage.getHTML = function () {
    var h = bobj.html;
    var isBorderBoxModel = bobj.isBorderBoxModel();
    
    var layerStyle = {
       width: '100%',
       height: '100%',
       overflow: 'auto',
       position: 'absolute',
       'text-align' : 'center' // For page centering in IE quirks mode
    };
    
    var pageOuterHeight = this.height + this.topMargin + this.bottomMargin;
    var pageOuterWidth = this.width + this.leftMargin + this.rightMargin;
    
    var contentHeight = isBorderBoxModel ? pageOuterHeight : this.height;
    var contentWidth = isBorderBoxModel ? pageOuterWidth : this.width;
    
    var positionStyle = {
       left: '4px',
       width: pageOuterWidth + 'px',
       height: pageOuterHeight + 'px',
       margin: '0 auto',  // center the page horizontally
       'text-align' : 'left',
       top: '4px',
       overflow: 'visible',
       position: 'relative'
       
    };
    
    if(bobj.isSafari() == true) {
        positionStyle['display'] = 'table';
    }
    
    var pageStyle = {
        position: 'relative',
        width: contentWidth + 'px',
        height: contentHeight + 'px',
        'z-index': 1,
        'border-width' : '1px',
        'border-style' : 'solid',
        'background-color': this.bgColor,     
        'padding-top': this.topMargin + 'px',
        'padding-right': this.rightMargin + 'px',
        'padding-left': this.leftMargin + 'px',
        'padding-bottom': this.bottomMargin + 'px'
    };
   
    var shadowStyle = {
        position: 'absolute', 
        filter: "progid:DXImageTransform.Microsoft.Blur(PixelRadius='2', MakeShadow='false', ShadowOpacity='0.75')",
        'z-index': 0,
        width: pageOuterWidth + 'px',
        height: pageOuterHeight + 'px',
        margin: '0 auto',
        left: (isBorderBoxModel ? 2 : 6) + 'px',
        top: (isBorderBoxModel ? 2 : 6) + 'px'
    };
    var shadowHTML = '';
    if(this.documentView.toLowerCase() == bobj.crv.ReportPage.DocumentView.PRINT_LAYOUT) {       
       layerStyle['background-color'] = '#8E8E8E';
       pageStyle['border-color'] = '#000000';
       shadowStyle['background-color'] = '#737373';
       shadowHTML = h.DIV({id : this.id + '_shadow', 'class': 'menuShadow', style:shadowStyle});
    }
    else {
       // Web Layout
       layerStyle['background-color'] = '#FFFFFF';
       pageStyle['border-color'] = '#FFFFFF';    
    }
           
    var html = h.DIV({id: this.id, style: layerStyle, 'class' : 'insetBorder'},
        h.DIV({id: this.id + '_pageCtn', style:positionStyle},
            h.DIV({id:this.id + '_page', style:pageStyle}),
            shadowHTML ));
   
    return html + bobj.crv.getInitHTML(this.widx);
};

bobj.crv.ReportPage.init = function () { 
    this._pageCtnNode = getLayer(this.id + '_pageCtn');  
    this._pageNode = getLayer(this.id + '_page');  
    this._shadowNode = getLayer(this.id + '_shadow');
    
    this.initOld();
    
    if (this.contentId) {
        var content = document.getElementById(this.contentId);
        if (content) {
            this.setHTML(content);
        }
    }
};

/**
 * Resizes the outer dimensions of the widget.
 */
bobj.crv.ReportPage.resize = function (w, h) {
    bobj.setOuterSize(this.layer, w, h);
    if(_moz) {
        this.css.clip = bobj.getRect(0,w,h,0);
    }
};

/**
 * @return Returns an object with width and height properties such that there 
 * would be no scroll bars around the page if they were applied to the widget. 
 */
bobj.crv.ReportPage.getBestFitSize = function() {
    var page = this._pageNode;
    return {
        width: page.offsetWidth + 30, 
        height: page.offsetHeight + 30 
    };
};
