(function() {
	var evt, t;
	t = function() {
		var scr;
		scr = document.scripts[document.scripts.length - 1];
		return scr.src.substr(0, scr.src.lastIndexOf("/"))
	}(),
	evt = function() {
		function evt(e) {
			this.el = e.el
			this.options = e.defaults
			this.source = e
			this._init(this.el)
		};
		evt.prototype.reset = function() {
			this.el.removeClass("waiting");
			this.el.removeClass("success");
			this.el.removeClass("only-read");
			this.el.addClass("ready");
			return this.el.find(".lr-img").css('backgroundImage','null');
		};
		evt.prototype.setWait = function() {
			this.el.removeClass("ready");
			this.el.removeClass("success");
			this.el.removeClass("only-read");
			return this.el.addClass("waiting");
		};
		evt.prototype.setImgOnly = function(e) {
			this.el.removeClass("ready");
			this.el.removeClass("waiting");
			this.el.removeClass("success");
			this.el.addClass("only-read");
			return this.el.find(".lr-img").css('backgroundImage',"url(" + e + ")");
		};
		evt.prototype.setImg = function(e) {
			this.setSuccess();
			return this.el.find(".lr-img").css('backgroundImage', "url(" + e + ")");
		};
		evt.prototype.setSuccess = function() {
			this.el.removeClass("ready");
			this.el.removeClass("waiting");
			this.el.removeClass("only-read");
			return this.el.addClass("success");
		};
		evt.prototype.setTitle = function(e) {
			return e ? this.el.find(".lr-title").html(e) : this.el.addClass("no-title");
		};
		evt.prototype._addEl = function() {
			var box;
			box = '<div class="lr-title"></div>\n\n<div class="lr-hint">\n<div class="lr-ready">\n<i class="fa fa-camera"></i>\n</div>\n\n<div class="lr-waiting">\n<i class="fa fa-spinner fa-spin"></i>\n</div>\n</div>\n\n<div class="lr-success">\n<div class="lr-remove">\n<i class="fa fa-trash"></i>\n</div>\n\n<div class="lr-img"></div>\n</div>'
			this.el.addClass("lr");
			this.el.addClass("lr-ui");
			this.el.html(box);
			return this.reset()
		};
		evt.prototype._onRemove = function() {
			return this.el.find(".lr-remove").bind("click", function(e) {return function() {return e.source.reset()}}(this));
		};
		evt.prototype._initcss = function() {
			var s;
			s = document.createElement("link");
			s.type = "text/css";
			s.rel = "stylesheet";
			s.href = "" + t + "/css/localResize.css";
			document.head.appendChild(s);
			return evt.prototype._initcss = function() {
				return !0
			}
		};
		evt.prototype._init = function() {
			return this._initcss(), this._addEl(), this._onRemove(), this.setTitle()
		};
		return evt;
	}(),
	window.LrUI = evt;
}).call(this);
(function() {
	var t;
	tag = function() {
		function tag(t, e) {
			var n, i;
			if (null == e && (e = {}), !t) return alert("aaaaa");
			this.el = t;
			this.results = {};
			this.defaults = {
				rWidth: 800,
				quality: 1.0,
				UI: !0
			};
			for (n in e) i = e[n], i && (this.defaults[n] = i);
			this._init()
		}
		tag.prototype.setImg = function(t) {
			this.UI.setImg(t);
			return this
		};
		tag.prototype.setImgOnly = function(t) {
			this.UI.setImgOnly(t);
			return this;
		};
		tag.prototype.setStop = function() {
			this.UI.setSuccess();
			return this;
		};
		tag.prototype.reset = function() {
			this.results = {};
			this.UI.reset();
			return this;
		};
		tag.prototype.change = function(t) {
			"function" == typeof t && (this.change = t);
			return this;
		};
		tag.prototype.success = function(t) {
			"function" == typeof t && (this.success = t);
			return this;
		};
		tag.prototype._initcss = function() {
			var e,n;
			e = ".lr {\n  position: absolute;\n  display: inline-block;\n  width:100%;\n height:100%;\n}\n\n.lr input[type=file] {\n    width: 100%;\n    height: 100%;\n    position: absolute;\n    top: 0;\n    left: 0;\n    z-index: 1991;\n    opacity: 0;\n}";
			n = document.createElement("style");
			n.innerHTML = e;
			document.head.appendChild(n);
			return tag.prototype._initcss = function() {
				return !0
			}
		};
		tag.prototype._createEl = function() {
			this.el.addClass("lr");
			this.defaults.UI && (this.UI = new LrUI(this));
			this.file = document.createElement("input");
			this.file.type = "file";
			return this.el.append(this.file);
		};
		tag.prototype._getBase64 = function() {
			var e;
			this.file.addEventListener("change", function() {return e(this.files[0]), this.value = ""}, !1);
			return e = function(e) {
				return function(n) {
					var i, s, o, r, a, u;
					i = window.URL || window.webkitURL || window.mozURL || window.msURL, s = e.defaults, a = e.results = {}, a.original = {};
					for (r in n){ u = n[r]; "function" != typeof u && (a.original[r] = u);}
					a.blob = i.createObjectURL(n);
					e.change(a);
					e.defaults.UI && (e.setImg(a.blob), e.UI.setWait());
					o = new Image;
					o.src = a.blob;
					return o.onload = function() {
						var n, i, r, u, l, c, h, p, f, d, y, g;
						p = o.width / o.height;
						g = s.rWidth || o.width;
						c = g / p;
						i = document.createElement("canvas");
						i.width = g;
						i.height = c;
						r = i.getContext("2d");
						r.drawImage(o, 0, 0, g, c);
						y = navigator.userAgent;
						n = null;
						if (y.match(/iphone/i)){
							try {
								h = new MegaPixImage(o), h.render(i, {maxWidth: g,maxHeight: c,quality: s.quality});
								n = i.toDataURL("image/jpeg", s.quality)
							} catch (m) {
								u = m, alert("bbbbb")
							}
						}else if (y.match(/Android/i)){
							try {
								l = new JPEGEncoder, n = l.encode(r.getImageData(0, 0, g, c), 100 * s.quality)
							} catch (m) {
								u = m, alert("ccccc")
							}
						} else { n = i.toDataURL("image/jpeg", s.quality); }
						$("#NewFaceImg").val(n);
						$("#faceImg").attr("src", n);
						
						a.base64 = n;
						a.base64Clean = n.substr(n.indexOf(",") + 1);
						return tag.prototype.success !== e.success ? (d = e, f = function() {function t(){};t.prototype.stop = function() {return d.setStop.call(d)};return t}(), e.success((new f).stop, a)) : void 0;
					}
				}
			}(this)
		};
		tag.prototype._init = function() {
			return this._initcss(), this._createEl(), this._getBase64()
		};
		return tag;
	}(),
	window.LocalResize = tag
}).call(this);
