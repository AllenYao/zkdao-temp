ZOL.widget=ZOL.widget||{};ZOL.widget.modStats=function(c){var d=this;this.config={modules:[],startTime:0,endTime:0,target:"http://888.zol.com.cn/images/pv008.gif?url="};for(key in c){this.config[key]=c[key]}this.findPid=function(g){var b=g.parentNode;var h=d.config.modules;if(!b){return false}var a=b.id;if(a){if(!h||!h.length){return a}for(key in d.config.modules){if(h[key]==a){return a}}}if(!b.tagName){return false}while(b.tagName.toLowerCase()!="body"){return d.findPid(b)}return a};this.findUrl=function(b){if(b.tagName.toLowerCase()=="a"){return{link:b.href,text:b.innerHTML.replace(/<.+?>/gim,"")}}var a=b.parentNode;if(!a||!a.tagName){return false}while(a.tagName.toLowerCase()!="body"){return d.findUrl(a)}};this.ckTime=function(){var a=d.config.startTime;var f=d.config.endTime;if(!a&&!f){return true}a=typeof(a)!="number"?Date.parse(a):a;f=typeof(f)!="number"?Date.parse(f):f;var b=(new Date()).getTime();if(a<b&&f>b){return true}else{return false}};this.stats=function(k,l){var b=escape(k.link);var a=escape(k.text);var i=escape(ZOL.URL);var j=[d.config.target+b,"text="+a,"ref="+i,"width="+screen.width,"mod="+l,"rand="+(new Date()).getTime()].join("&");(new Image()).src=j;return true};this.init=function(){document.onmousedown=function(a){a=window.event||a;var b=a.srcElement||a.target;var h=d.findPid(b);var e=d.findUrl(b);if(h&&e&&e.link&&d.ckTime()){d.stats(e,h)}}}};