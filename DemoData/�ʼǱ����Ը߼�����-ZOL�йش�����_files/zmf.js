/**
* @author wiki <wu.kun@zol.com.cn>
* @copyringt (c) ZOL tech GROUP
* @version ZMF v1.0 beta 1.0
*/

//����
//document.domain = "zol.com.cn";
//������
window.onerror = function () {
	//return true;
};

if (typeof ZOL == "undefined" || !ZOL) {
	var ZOL = {};
}

ZOL.namespace = function() {
	var a=arguments, o=null, i, j, d;
	for (i=0; i<a.length; i=i+1) {
		d=a[i].split(".");
		o=ZOL;

		for (j=(d[0] == "ZOL") ? 1 : 0; j<d.length; j=j+1) {
			o[d[j]]=o[d[j]] || {};
			o=o[d[j]];
		}
	}

	return o;
};

(function() {
	ZOL.namespace("util", 'widget');
})();
ZOL.config = {
	root : 'http://icon.zol-img.com.cn/products/js/',
	loadRoot : ''
};
/**
* @param {Object} target Ŀ�����
* @param {Object} source Դ����
* @param {boolean} deep �Ƿ���(�̳�)�����еĶ���
* @return {Object} ���ؼ̳���source�������Ե��¶���
*/
ZOL.extend = function(target, source, deep)
{
	target = target || {};
	var sType = typeof source, i = 1, options;
	if (sType === 'undefined' || sType === 'boolean') {
		deep = sType === 'boolean' ? source : false;
		source = target;
		target = this;
	}
	if(typeof source !== 'object' && Object.prototype.toString.call(source).call(source) !== '[object Function]')
		source = {};
   
	while (i <= 2) {
		options = i === 1 ? target : source;
		if (options != null) {
			for( var name in options ) {
				var src = target[name], copy = options[name];
				if(target === copy) {
					continue;
				}
				if (deep && copy && typeof copy === 'object' && !copy.nodeType) {
					target[name] = this.extend(src ||
							(copy.length != null ? [] : {}), copy, deep);
				} else if (copy !== undefined) {
					target[name] = copy;
				}
			}
		}
		i++;
	}
	return target;
};

ZOL.W = window;
ZOL.D = document;

ZOL.URL = ZOL.D.location.href;//����URL��ַ
ZOL.go = function (url) {
	ZOL.D.location.href = url;
};

ZOL.$ = function(id) {
	
	if (!id) {
		return false;
	}
	
	if (typeof(id) == 'object') {
		return id;
	}
	
	var isIE = ZOL.browser.IE;
	if (isIE && isIE < 8) {//IE�������id��NAME��ͻ���
		elems = document.all[id];
		if (elems) {
			for (var i = 0, len = elems.length; i < len; i ++)
			{
				var elem = elems[i];
				if (elem.id == id) {
					return elem;
				}
			}
		} else {
			return false;
		}
	}
	
	return !!id && document.getElementById(id) || false;
}
/**
* ����tagname����DOM����
* @param string tagName ��ǩ��
* @param string|htmlElement context ������
*/
ZOL.find = function (tagName, contextId)
{
	if (!tagName || typeof(tagName) != 'string') {
		return false;
	}
	var context = ZOL.$(contextId);
    //����������⣬��������contextId�Ҳ���������ת����document�������ǲ���ȷ��
    if(contextId && !context){ //���������contextId������û���ҵ����ͷ��أ�����ֵΪZOL.D
        return false;
    }
	context = context || ZOL.D;
	
	if (typeof(context) != 'object') {
		return false;
	}
	return context.getElementsByTagName(tagName);
};
/**
* ����ClassName����DOM��������
* @param string className ��ǩ��
* @param string|htmlElement context ������
* @param string tagName ��ǩ��
*/
ZOL.findByClass = function(className , contextId , tagName){
   	var ele=[];
	var context = ZOL.$(contextId);
    if(contextId && !context){
        return false;
    }
	context = context || ZOL.D;

    var all=context.getElementsByTagName(tagName||"*");
	for(var i=0;i<all.length;i++){
        if(all[i].className.match(new RegExp('(\\s|^)'+className+'(\\s|$)'))){
            ele[ele.length]=all[i];
        }
	}
	return ele;
}
/**
* ѭ������ÿ������
*/
ZOL.each = function(arr, callback)
{
	var len = arr.length;
	if (!len || typeof(callback) !== 'function') {
		return false;
	}
	
	for (var i = 0; i < len; i++) {
		callback(arr[i], i);
	}
	return true;
};

/**
* ������汾��� ����yui��YAHOO.env.ua���޸�
* @class ZOL.browser
* @static
*/
ZOL.browser = function ()
{
	var o = {
		IE     : 0,
		OPERA  : 0,
		GECKO  : 0,
		WEBKIT : 0,
		FIREFOX: 0,
		MOBILE : null
	};
	
	var ua=navigator.userAgent, m;
	
	if ((/KHTML/).test(ua)) {
		o.WEBKIT=1;
	}
	m=ua.match(/AppleWebKit\/([^\s]*)/);
	
	if (m&&m[1]) {
		o.WEBKIT=parseFloat(m[1]);

		// Mobile browser check
		if (/ Mobile\//.test(ua)) {
			o.MOBILE = "Apple"; // iPhone or iPod Touch
		} else {
			m=ua.match(/NokiaN[^\/]*/);
			if (m) {
				o.MOBILE = m[0]; // Nokia N-series, ex: NokiaN95
			}
		}

	}

	if (!o.WEBKIT) { // not webkit
		// @todo check Opera/8.01 (J2ME/MIDP; Opera Mini/2.0.4509/1316; fi; U; ssr)
		m=ua.match(/Opera[\s\/]([^\s]*)/);
		if (m&&m[1]) {
			o.OPERA=parseFloat(m[1]);
			m=ua.match(/Opera Mini[^;]*/);
			if (m) {
				o.MOBILE = m[0]; // ex: Opera Mini/2.0.4509/1316
			}
		} else { // not opera or webkit
			m=ua.match(/MSIE\s([^;]*)/);
			if (m&&m[1]) {
				o.IE=parseFloat(m[1]);
			} else { // not opera, webkit, or ie
				m=ua.match(/Gecko\/([^\s]*)/);
				if (m) {
					o.GECKO=1; // Gecko detected, look for revision
					m=ua.match(/rv:([^\s\)]*)/);
					if (m&&m[1]) {
						o.GECKO=parseFloat(m[1]);
					}
				}
				m = ua.match(/Firefox\/([^\s]*)/);
				if (m && m[1]) {
					o.FIREFOX = m[1];
				}
			}
		}
	}
	return o;
}();

/**
* ϵͳ����
*/
ZOL.env = {
	trim : function (str)
	{
		return str.replace(/(^\s*)|(\s*$)/g, '');
	},
	
	isUndefined : function (o)
	{
		return typeof(o) === 'undefined';
	},
	
	isnull : function (exp)
	{
		return exp === null;
	},
	
	isset : function (exp)
	{
		return exp == undefined;
	},
	strlen : function(str)
	{
		return str.replace(/[^\x00-\xff]/g, '..').length;
	},
	getRewriteJsUrl : function (ajaxUrl)
	{
		if (!ajaxUrl) {
			return false;
		}
		ajaxUrl = ajaxUrl.replace(/index\.php\?c=Ajax\&a=([\w_]+)\&(.+)/g, "ajax_$1_$2.html");
		ajaxUrl = ajaxUrl.replace(/(&|%26)/g, '%5E');//�滻&Ϊ^
		return ajaxUrl;
	},
    escape : function(str)
    {
        return escape(str).replace(/%/g, '^');
    }
	
}

ZOL.load = function(file, filetype, callback) {
	var self = this;
	this.loaded = false;
	this.callback = callback;//�ص�����
	filetype = filetype || 'js';
	file = file.indexOf('http://') == 0 ? file : (ZOL.config.loadRoot + file);
	var onload = function (file)
	{
		typeof(callback) == 'function' && callback(file);
	}
	
	var isLoading = function(elem)
	{
		return elem.readyState && elem.readyState=="loading";
	};
	
	var checkLoaded = function ()
	{
		switch (filetype) {
			case 'js' :
				tagName      = 'script';
				linkAttrName = 'src';
				break;
			case 'css' :
				tagName      = 'link';
				linkAttrName = 'href';
				break;
			default :
				return false;
				break;
		}
		
		var loadElems = document.getElementsByTagName(tagName);
		if (loadElems) {
			for (var i = 0, len = loadElems.length; i < len; i++) {
				var elem = loadElems[i];
				if (elem[linkAttrName].indexOf(file) != -1 && !isLoading(elem)) {
					self.loaded = true;
					return true;
				}
			}
		}
	}
	
	if (checkLoaded()) {
		onload(file);
		return;
	}
	
	if (filetype == 'js') {
		var elem = document.createElement('script');
		with(elem) {
			type = 'text/javascript';
			src = file;
		}
	} else if (filetype == 'css') {
		var elem = document.createElement("link");
		with(elem) {
			type = 'text/css';
			rel  = 'stylesheet';
			href = file;
		}
	}
	
	
	elem && document.getElementsByTagName("head")[0].appendChild(elem);
	
	var loadFunc = function()
	{
		if(isLoading(this))
			return;
		else
		  onload(file);
	}
	
	elem.onload=elem.onreadystatechange=loadFunc;
	
	return elem;
};

ZOL.addEvent = function(obj, eventType, fn)
{
	var obj = (typeof(obj) == 'string') ? ZOL.$(obj) : obj;
	if (!obj) {
        return false;
    }
	if(obj.attachEvent) {
		
		var typeRef = "_" + eventType;
		if (!obj[typeRef]) {
			obj[typeRef] = [];
			var onEvent = obj['on' + eventType];
			onEvent && obj[typeRef].push(onEvent);
		}
		
		for (var i in obj[typeRef]) {
			if (obj[typeRef][i] == fn) {
				return;
			}
		}
		
		obj[typeRef].push(fn);
		obj['on' + eventType] = function()
		{
			
			for (var i in this[typeRef]) {
				if (this[typeRef][i].apply(this, arguments) === false) {
					return false;
				}
			}
		}
	}else{
		obj.addEventListener(eventType, fn, false);
	}
};
ZOL.removeEvent = function(obj, eventType, fn)
{
	var obj = (typeof(obj) == 'string') ? ZOL.$(obj) : obj;
	if (!obj) {
        return false;
    }
	if (obj.attachEvent) {
		var typeRef = "_" + eventType;
		if (!obj[typeRef]) {
			return false;
		}
		for (var i in obj[typeRef]) {
			if (obj[typeRef][i] == fn) {
				obj[typeRef].splice(i, 1);
				break;
			}
		}
	} else {
		obj.removeEventListener(eventType, fn, false);
	}
}

ZOL.addClass = function(obj, className)
{
	var obj = (typeof(obj) == 'string') ? ZOL.$(obj) : obj;
	ZOL.removeClass(obj, className);
	obj.className += ' ' + className;
	return true;
};
ZOL.removeClass = function(obj, className)
{
	var obj = (typeof(obj) == 'string') ? ZOL.$(obj) : obj;
	
	if (!className) {
		obj.className = '';
		return true;
	}
	
	var classNames = className.split(' ');
	var _className = ' ' + obj.className + ' ';
	for (var i in classNames) {
		_className = _className.replace(' ' + classNames[i] + ' ', ' ');
	}
	
	obj.className = ZOL.env.trim(_className);
	return true;
}

ZOL.onReady = function(fn)
{
	if (typeof(fn) != 'function') {
		return false;
	}
	this.domReady = false;
	
	if (typeof(fns) == 'undefined') {
		var fns = [];
	}
	fns.push(fn);
	
	var init = function()
	{
		for (i in fns) {
			fns[i]();
		}
	};
	this.ready = function()
	{
		if (this.domReady) {
			init();
		}
		
		if (ZOL.browser.IE) {
			var timer = window.setInterval(function()
			{
				try {
					document.body.doScroll('left');
					init();
					window.clearInterval(timer);
					this.domReady = true;
				} catch (e) {}
			}, 5);
		} else {
			try {
				document.removeEventListener('DOMContentLoaded', init);
			} catch (e) {}
			
			document.addEventListener('DOMContentLoaded', init, false);
			this.domReady = true;
		}
	}
	this.ready();
}

ZOL.util.cookie = {
	get : function(n){
		var v = '',
		c = ' ' + document.cookie + ';',
		s = c.indexOf((' ' + n + '='));
		if (s >= 0) {
			s += n.length + 2;
			v = unescape(c.substring(s, c.indexOf(';', s)));
		}
		return v;
	},
	set : function(n,v){
		var a=arguments,al=a.length;
		document.cookie = n + "=" + v +
		((al>2&&a[2]!="") ? ";expires=" + ((new Date((new Date()).getTime() + a[2] * 3600000)).toGMTString()) : "") + ";path=" + ((al>3&&a[3]!="") ? a[3] : "/") +
		";domain="  + ((al>4&&a[4]!="") ? a[4] : ".zol.com.cn");
	},
	checksub : function(sCookie,s){
		var aParts = sCookie.split('&'),nParts = aParts.length,aKeyVal;
		if (nParts==1) {
			return sCookie.indexOf(s);
			} else {
			for(var i=0; i<nParts; i++){
				aKeyVal = aParts[i].split('=');
				if(aKeyVal[0]==s){
					return i;
				}
			}
		}
		return -1;
	},
	getsub : function(n,s){
		var sCookie = this.get(n);
		var nExists = this.checksub(sCookie,s);
		if (nExists>-1) {
			return sCookie.split('&')[nExists].split('=')[1];
			} else if (sCookie.indexOf(s)>0) {
			return sCookie.split('=')[1];
		}
		return '';
	},
	setsub : function(n,s,v){
		var sCookie = this.get(n),a=arguments,al=a.length;
		var aParts = sCookie.split('&');
		var nExists = this.checksub(sCookie,s);
		if (sCookie=='') {
			sNewVal=(s+'='+v).toString();
			} else {
			if(nExists==-1){nExists=aParts.length;}
			aParts[nExists]=s+'='+v;
			sNewVal = aParts.join('&');
		}
		return this.set(n,sNewVal,(a[3]||''),(a[4]||'/'),(a[5]||''));
	}
};

//�����ղ�
ZOL.util.addFavorite = function (obj) {
	
	var url=document.location.href;
	var title=document.title;
	if (document.all) {
		window.external.AddFavorite(url,title);
	} else if (window.sidebar) {
		window.sidebar.addPanel(title,url,"");
	} else if ( window.opera && window.print ) {
		obj.setAttribute('rel','sidebar');
		obj.setAttribute('href',url);
		obj.setAttribute('title',title);
	}
	return false;
};

//���ƹ���
ZOL.util.copy = function (data, callback)
{
	if (window.clipboardData){
		if(window.clipboardData.setData("Text",data)){
			typeof(callback) == 'function' && callback();
		}
	}else{
//        try
//        {
//            netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
//        }
//        catch (e)
//        {
//            alert("����firefox��ȫ�������������м�������������ڵ�ַ�������롰about:config������signed.applets.codebase_principal_support������Ϊ��true��֮������");
//            return false;
//        }
//
		var flashcopier = 'flashcopier';
		var swfFile = '/_clipboard.swf';
		if(!ZOL.$(flashcopier)) {
			var divholder = document.createElement('div');
			divholder.id = flashcopier;
			divholder.style.display='none';
			document.body.appendChild(divholder);
		}
		ZOL.$(flashcopier).innerHTML = '';
		data = data.replace(/\"/g,'\'');
		var divinfo = '<embed src="' + swfFile + '" FlashVars="clipboard='+data+'" width="0" height="0" type="application/x-shockwave-flash"></embed>';
        ZOL.$(flashcopier).innerHTML = divinfo;
       alert('��ǰ������޷��Զ����Ƶ������壬��ʹ��Ctrl+c��ݼ��ֶ�����\r\n'+data);
		//typeof(callback) == 'function' && callback();
   }
};

//��ȡ����
ZOL.util.getPosition = function (elem) {
	if (!elem) return false;
	
	var x = elem.offsetLeft;
	var y = elem.offsetTop;
	var w = elem.offsetWidth;
	var h = elem.offsetHeight;
	while (elem = elem.offsetParent) {
		x += elem.offsetLeft;
		y += elem.offsetTop;
	}
	
	return {x : x, y : y, w : w, h : h};
};

//��ȡҳ�����������Ϣ
ZOL.util.getBodySize = function ()
{
	var doc = document.documentElement, body = document.body;
	//��ҳ��
	var aw = doc && doc.scrollWidth || body && body.scrollWidth || 0;
	//��ҳ��
	var ah = doc && doc.scrollHeight || body && body.scrollHeight || 0;
	//���ӿ�
	var w = doc && doc.clientWidth || body && body.clientWidth || 0;
	//���Ӹ�
	var h = doc && doc.clientHeight || body && body.clientHeight || 0;
	//��ȥ���������
	var x = doc && doc.scrollLeft || body && body.scrollLeft || 0;
	//��ȥ�Ĵ�ֱ������
	var y = doc && doc.scrollTop || body && body.scrollTop || 0;
	return {aw : aw, ah : ah, w : w, h : h, x : x, y : y};
};

ZOL.util.mouse = function(e)
{
	var e = e || window.event;
	var bodyPos = ZOL.util.getBodySize();
	var x = e.pageX || (e.clientX ? (e.clientX + bodyPos.x) : null);
	var y = e.pageY || (e.clientY ? (e.clientY + bodyPos.y) : null);
	var rx = e.screenX;
	var ry = e.screenY;
	return {x:x, y:y, rx:rx, ry:ry};
};

//ZOL�û���Ϣ
ZOL.user = function ()
{
	var info = {
		sid : 0,
		id : '',
		nickName : ''
	};
	
	var cookie = ZOL.util.cookie;
	info.sid = cookie.get('zol_sid');
	info.id = cookie.get('zol_userid');
	info.nickName = cookie.get('zol_nickname');
	return info;
}();

ZOL.util.toggle = function(id, styles, callback) 
{
	var obj = ZOL.$(id);
	styles = styles || {};
	var show = styles.show || '';
	var hidden = styles.hidden || 'none';
	if(obj.style.display == show){
		obj.style.display = hidden;
	} else {
		obj.style.display = show;
	}
	typeof(callback) == 'function' && callback(obj.style.display);
	
};

// ��¼������
var loginFrom = null;
ZOL.widget.login = {
	show : function() {
		new ZOL.load(ZOL.config.root + 'util/dialog.js','js', function()
		{
			var login_str = [
				'<form target="_top" method="post" action="http://service.zol.com.cn/user/login.php" name="log_form">',
				'<input type="hidden" value="login" name="ACT"/>',
				'<input type="hidden" value="'+ZOL.URL+'" name="backUrl"/>',
				'<dl>',
				'<dt>��¼��</dt>',
				'<dd><input type="text" class="login" name="userid"></dd>',
				'<dt>����</dt>',
				'<dd><input type="password" class="login" name="pwd"></dd>',
				'</dl>',
				'<dl>',
				'<input type="submit" value="ȷ��" />',
				'<input type="button" value="ȡ��" onclick="loginFrom.hidden()"/>',
				'</dl>',
				'<dl>',
				'<a href="http://service.zol.com.cn/user/get_pwd.php">�������룿</a>',
				'</dl>',
				'<dl class="line">',
				'</dl>',
				'<dl>',
				'<input type="submit" style="float: none;" onclick="location.href=\'http://service.zol.com.cn/user/register.php\';" value="����ע��ZOL�ʺ�" name="submit"/>',
				'</dl>',
				'</form>'
				].join("\r\n");
			var config = {
						border : '1px solid #000',
						cssfile : 'http://icon.zol-img.com.cn/products/css/login.css',
						width : '240px',
						height : '230px',
						title  : '<b id="login_title">ZOL�û����¼���������</b>',
						buttons : {}
					}
			loginFrom = new ZOL.widget.dialog('login_form', config);
			loginFrom.config.content = login_str;
			loginFrom.show();
		});
	}
};
/*ԭͼҳ��ʽ*/
ZOL.widget.piclogin = {
	show : function() {
		new ZOL.load(ZOL.config.root + 'util/dialog.js','js', function()
		{
			var login_str = [
				'<form target="_top" method="post" action="http://service.zol.com.cn/user/login.php" name="log_form">',
				'<input type="hidden" value="login" name="ACT"/>',
				'<input type="hidden" value="'+ZOL.URL+'" name="backUrl"/>',
                '<div class="loginMsg">��ܰ��ʾ����¼�󷽿����ԭʼ��ͼ</div>',
				'<div class="loginName"><span>��¼����</span><input type="text" class="login" name="userid"></div>',
				'<div class="loginPass"><span>�ܡ��룺</span><input type="password" class="login" name="pwd"></div>',
				'<div class="loginTip"><lable><input type="checkbox"></lable> �Զ���¼',
                '<a href="http://service.zol.com.cn/user/get_pwd.php">�������룿</a></div>',
                '<div class="loginSub"><input class="dl_bg" type="submit" value="" />',
                '<a href="http://service.zol.com.cn/user/register.php">ע��ZOL�ʺ�</a></div>',
				'</form>'
				].join("\r\n");
			var config = {
                        defaultCssFile : 'http://icon.zol-img.com.cn/products/css/piclogin.css',
                        padding: '0',
						width : '415px',
						height : '260px',
						title  : '<b id="login_title">�û���¼</b>',
						buttons : {}
					}
			loginFrom = new ZOL.widget.dialog('login_form', config);
			loginFrom.config.content = login_str;
			loginFrom.show();
		});
	}
};

/**
* ê��(Anchor)��ƽ���ƶ�
* wanghb 2010-12-08
* el : Ŀ��ê�� ID
* duration : ����ʱ�䣬�Ժ���Ϊ��λ��ԽСԽ��
*/
ZOL.util.scroller = function(el, duration) {
    if (typeof el != 'object') {
        el = ZOL.$(el);
    }
    if (!el) {
        return;
    }

    var z = this;
    z.el = el;
    z.p = ZOL.util.getPosition(el);//�������
    z.s = ZOL.util.getBodySize();//��ù�����
    z.clear = function() {window.clearInterval(z.timer);z.timer = null};
    z.t = (new Date).getTime();

    z.step = function() {
        var t = (new Date).getTime();
        var p = (t - z.t) / duration;
        if (t >= duration + z.t) {
            z.clear();
            window.setTimeout(function() {z.scroll(z.p.y, z.p.x)}, 13);
        } else {
            st = ((-Math.cos(p * Math.PI) / 2) + 0.5) * (z.p.y - z.s.y) + z.s.y;
            //sl = ((-Math.cos(p * Math.PI) / 2) + 0.5) * (z.p.x - z.s.x) + z.s.x;
            sl = 0;
            z.scroll(st, sl);
        }
    };

    z.scroll = function(t, l) {window.scrollTo(0, t)};
    z.timer = window.setInterval(function() {z.step();}, 13);
};
ZOL.autoOnclick = function(obj)
{
    if(ZOL.browser.IE){
       obj.onclick();
    }else{
      var evt = document.createEvent("MouseEvents");
      evt.initEvent("click",true,true);
      obj.dispatchEvent(evt);
   }
};