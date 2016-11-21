/*
 * SystemJS v0.19.40
 */
!function () {
    function e() {
        !function (e) { function t(e, r) { if ("string" != typeof e)
            throw new TypeError("URL must be a string"); var n = String(e).replace(/^\s+|\s+$/g, "").match(/^([^:\/?#]+:)?(?:\/\/(?:([^:@\/?#]*)(?::([^:@\/?#]*))?@)?(([^:\/?#]*)(?::(\d*))?))?([^?#]*)(\?[^#]*)?(#[\s\S]*)?/); if (!n)
            throw new RangeError("Invalid URL format"); var a = n[1] || "", o = n[2] || "", i = n[3] || "", s = n[4] || "", l = n[5] || "", u = n[6] || "", d = n[7] || "", c = n[8] || "", f = n[9] || ""; if (void 0 !== r) {
            var m = r instanceof t ? r : new t(r), p = !a && !s && !o;
            !p || d || c || (c = m.search), p && "/" !== d[0] && (d = d ? (!m.host && !m.username || m.pathname ? "" : "/") + m.pathname.slice(0, m.pathname.lastIndexOf("/") + 1) + d : m.pathname);
            var h = [];
            d.replace(/^(\.\.?(\/|$))+/, "").replace(/\/(\.(\/|$))+/g, "/").replace(/\/\.\.$/, "/../").replace(/\/?[^\/]*/g, function (e) { "/.." === e ? h.pop() : h.push(e); }), d = h.join("").replace(/^\//, "/" === d[0] ? "/" : ""), p && (u = m.port, l = m.hostname, s = m.host, i = m.password, o = m.username), a || (a = m.protocol);
        } d = d.replace(/\\/g, "/"), this.origin = s ? a + ("" !== a || "" !== s ? "//" : "") + s : "", this.href = a + (a && s || "file:" == a ? "//" : "") + ("" !== o ? o + ("" !== i ? ":" + i : "") + "@" : "") + s + d + c + f, this.protocol = a, this.username = o, this.password = i, this.host = s, this.hostname = l, this.port = u, this.pathname = d, this.search = c, this.hash = f; } e.URLPolyfill = t; }("undefined" != typeof self ? self : global), function (e) {
            function t(e, t) { if (!e.originalErr)
                for (var r = ((e.message || e) + (e.stack ? "\n" + e.stack : "")).toString().split("\n"), n = [], a = 0; a < r.length; a++)
                    "undefined" != typeof $__curScript && r[a].indexOf($__curScript.src) != -1 || n.push(r[a]); var o = "(SystemJS) " + (n ? n.join("\n\t") : e.message.substr(11)) + "\n\t" + t; F || (o = o.replace(D ? /file:\/\/\//g : /file:\/\//g, "")); var i = $ ? new Error(o, e.fileName, e.lineNumber) : new Error(o); return i.stack = o, i.originalErr = e.originalErr || e, i; }
            function r() { }
            function n(t) { this._loader = { loaderObj: this, loads: [], modules: {}, importPromises: {}, moduleRecords: {} }, q(this, "global", { get: function () { return e; } }); }
            function a() { n.call(this), this.paths = {}, this._loader.paths = {}, V.call(this); }
            function o() { }
            function i(e, t) { a.prototype[e] = t(a.prototype[e] || function () { }); }
            function s(e) { V = e(V || function () { }); }
            function l(e) { return e.match(Y); }
            function u(e) { return "." == e[0] && (!e[1] || "/" == e[1] || "." == e[1]) || "/" == e[0]; }
            function d(e) { return !u(e) && !l(e); }
            function c(e, t) { if ("." == e[0]) {
                if ("/" == e[1] && "." != e[2])
                    return (t && t.substr(0, t.lastIndexOf("/") + 1) || J) + e.substr(2);
            }
            else if ("/" != e[0] && e.indexOf(":") == -1)
                return (t && t.substr(0, t.lastIndexOf("/") + 1) || J) + e; return new H(e, t && t.replace(/#/g, "%05") || K).href.replace(/%05/g, "#"); }
            function f(e, t) { var r, n = "", a = 0, o = e.paths, i = e._loader.paths; for (var s in o)
                if (!o.hasOwnProperty || o.hasOwnProperty(s)) {
                    var l = o[s];
                    if (l !== i[s] && (l = o[s] = i[s] = c(o[s], u(o[s]) ? J : e.baseURL)), s.indexOf("*") === -1) {
                        if (t == s)
                            return o[s];
                        if (t.substr(0, s.length - 1) == s.substr(0, s.length - 1) && (t.length < s.length || t[s.length - 1] == s[s.length - 1]) && ("/" == o[s][o[s].length - 1] || "" == o[s]))
                            return o[s].substr(0, o[s].length - 1) + (t.length > s.length ? (o[s] && "/" || "") + t.substr(s.length) : "");
                    }
                    else {
                        var d = s.split("*");
                        if (d.length > 2)
                            throw new TypeError("Only one wildcard in a path is permitted");
                        var f = d[0].length;
                        f >= a && t.substr(0, d[0].length) == d[0] && t.substr(t.length - d[1].length) == d[1] && (a = f, n = s, r = t.substr(d[0].length, t.length - d[1].length - d[0].length));
                    }
                } var m = o[n]; return "string" == typeof r && (m = m.replace("*", r)), m; }
            function m(e) { for (var t = [], r = [], n = 0, a = e.length; n < a; n++) {
                var o = U.call(t, e[n]);
                o === -1 ? (t.push(e[n]), r.push([n])) : r[o].push(n);
            } return { names: t, indices: r }; }
            function p(t) { var r = {}; if (("object" == typeof t || "function" == typeof t) && t !== e)
                if (Q)
                    for (var n in t)
                        "default" !== n && h(r, t, n);
                else
                    g(r, t); return r.default = t, q(r, "__useDefault", { value: !0 }), r; }
            function h(e, t, r) { try {
                var n;
                (n = Object.getOwnPropertyDescriptor(t, r)) && q(e, r, n);
            }
            catch (n) {
                return e[r] = t[r], !1;
            } }
            function g(e, t, r) { var n = t && t.hasOwnProperty; for (var a in t)
                n && !t.hasOwnProperty(a) || r && a in e || (e[a] = t[a]); return e; }
            function v(e, t, r) { var n = t && t.hasOwnProperty; for (var a in t)
                if (!n || t.hasOwnProperty(a)) {
                    var o = t[a];
                    a in e ? o instanceof Array && e[a] instanceof Array ? e[a] = [].concat(r ? o : e[a]).concat(r ? e[a] : o) : "object" == typeof o && null !== o && "object" == typeof e[a] ? e[a] = g(g({}, e[a]), o, r) : r || (e[a] = o) : e[a] = o;
                } }
            function b(e, t, r, n, a) { for (var o in t)
                if (U.call(["main", "format", "defaultExtension", "basePath"], o) != -1)
                    e[o] = t[o];
                else if ("map" == o)
                    g(e.map = e.map || {}, t.map);
                else if ("meta" == o)
                    g(e.meta = e.meta || {}, t.meta);
                else if ("depCache" == o)
                    for (var i in t.depCache) {
                        var s;
                        s = "./" == i.substr(0, 2) ? r + "/" + i.substr(2) : P.call(n, i), n.depCache[s] = (n.depCache[s] || []).concat(t.depCache[i]);
                    }
                else
                    !a || U.call(["browserConfig", "nodeConfig", "devConfig", "productionConfig"], o) != -1 || t.hasOwnProperty && !t.hasOwnProperty(o) || w.call(n, '"' + o + '" is not a valid package configuration option in package ' + r); }
            function y(e, t, r, n) { var a; if (e.packages[t]) {
                var o = e.packages[t];
                a = e.packages[t] = {}, b(a, n ? r : o, t, e, n), b(a, n ? o : r, t, e, !n);
            }
            else
                a = e.packages[t] = r; return "object" == typeof a.main && (a.map = a.map || {}, a.map["./@main"] = a.main, a.main.default = a.main.default || "./", a.main = "@main"), a; }
            function w(e) { this.warnings && "undefined" != typeof console && console.warn; }
            function x(e, t) { for (var r = e.split("."); r.length;)
                t = t[r.shift()]; return t; }
            function S(e, t) { var r, n = 0; for (var a in e)
                if (t.substr(0, a.length) == a && (t.length == a.length || "/" == t[a.length])) {
                    var o = a.split("/").length;
                    if (o <= n)
                        continue;
                    r = a, n = o;
                } return r; }
            function _(e) { this._loader.baseURL !== this.baseURL && ("/" != this.baseURL[this.baseURL.length - 1] && (this.baseURL += "/"), this._loader.baseURL = this.baseURL = new H(this.baseURL, K).href); }
            function E(e, t) { this.set("@system-env", te = this.newModule({ browser: F, node: !!this._nodeRequire, production: !t && e, dev: t || !e, build: t, default: !0 })); }
            function j(e, t) { if (!d(e))
                throw new Error("Node module " + e + " can't be loaded as it is not a package require."); if (!re) {
                var r = this._nodeRequire("module"), n = t.substr(D ? 8 : 7);
                re = new r(n), re.paths = r._nodeModulePaths(n);
            } return re.require(e); }
            function P(e, t) { if (u(e))
                return c(e, t); if (l(e))
                return e; var r = S(this.map, e); if (r) {
                if (e = this.map[r] + e.substr(r.length), u(e))
                    return c(e);
                if (l(e))
                    return e;
            } if (this.has(e))
                return e; if ("@node/" == e.substr(0, 6)) {
                if (!this._nodeRequire)
                    throw new TypeError("Error loading " + e + ". Can only load node core modules in Node.");
                return this.builder ? this.set(e, this.newModule({})) : this.set(e, this.newModule(p(j.call(this, e.substr(6), this.baseURL)))), e;
            } return _.call(this), f(this, e) || this.baseURL + e; }
            function O(e, t, r) { te.browser && t.browserConfig && r(t.browserConfig), te.node && t.nodeConfig && r(t.nodeConfig), te.dev && t.devConfig && r(t.devConfig), te.build && t.buildConfig && r(t.buildConfig), te.production && t.productionConfig && r(t.productionConfig); }
            function k(e) { var t = e.match(oe); return t && "System.register" == e.substr(t[0].length, 15); }
            function M() { return { name: null, deps: null, originalIndices: null, declare: null, execute: null, executingRequire: !1, declarative: !1, normalizedDeps: null, groupIndex: null, evaluated: !1, module: null, esModule: null, esmExports: !1 }; }
            function R(t) { if ("string" == typeof t)
                return x(t, e); if (!(t instanceof Array))
                throw new Error("Global exports must be a string or array."); for (var r = {}, n = !0, a = 0; a < t.length; a++) {
                var o = x(t[a], e);
                n && (r.default = o, n = !1), r[t[a].split(".").pop()] = o;
            } return r; }
            function z(e) { var t, r, n, n = "~" == e[0], a = e.lastIndexOf("|"); return a != -1 ? (t = e.substr(a + 1), r = e.substr(n, a - n), n && w.call(this, 'Condition negation form "' + e + '" is deprecated for "' + r + "|~" + t + '"'), "~" == t[0] && (n = !0, t = t.substr(1))) : (t = "default", r = e.substr(n), se.indexOf(r) != -1 && (t = r, r = null)), { module: r || "@system-env", prop: t, negate: n }; }
            function I(e) { return e.module + "|" + (e.negate ? "~" : "") + e.prop; }
            function T(e, t, r) { var n = this; return this.normalize(e.module, t).then(function (t) { return n.load(t).then(function (a) { var o = x(e.prop, n.get(t)); if (r && "boolean" != typeof o)
                throw new TypeError("Condition " + I(e) + " did not resolve to a boolean."); return e.negate ? !o : o; }); }); }
            function L(e, t) { var r = e.match(le); if (!r)
                return Promise.resolve(e); var n = z.call(this, r[0].substr(2, r[0].length - 3)); return this.builder ? this.normalize(n.module, t).then(function (t) { return n.module = t, e.replace(le, "#{" + I(n) + "}"); }) : T.call(this, n, t, !1).then(function (r) { if ("string" != typeof r)
                throw new TypeError("The condition value for " + e + " doesn't resolve to a string."); if (r.indexOf("/") != -1)
                throw new TypeError("Unabled to interpolate conditional " + e + (t ? " in " + t : "") + "\n\tThe condition value " + r + ' cannot contain a "/" separator.'); return e.replace(le, r); }); }
            function C(e, t) { var r = e.lastIndexOf("#?"); if (r == -1)
                return Promise.resolve(e); var n = z.call(this, e.substr(r + 2)); return this.builder ? this.normalize(n.module, t).then(function (t) { return n.module = t, e.substr(0, r) + "#?" + I(n); }) : T.call(this, n, t, !0).then(function (t) { return t ? e.substr(0, r) : "@empty"; }); }
            var A = "undefined" == typeof window && "undefined" != typeof self && "undefined" != typeof importScripts, F = "undefined" != typeof window && "undefined" != typeof document, D = "undefined" != typeof process && "undefined" != typeof process.platform && !!process.platform.match(/^win/);
            e.console || (e.console = { assert: function () { } });
            var q, U = Array.prototype.indexOf || function (e) { for (var t = 0, r = this.length; t < r; t++)
                if (this[t] === e)
                    return t; return -1; };
            !function () { try {
                Object.defineProperty({}, "a", {}) && (q = Object.defineProperty);
            }
            catch (e) {
                q = function (e, t, r) { try {
                    e[t] = r.value || r.get.call(e);
                }
                catch (e) { } };
            } }();
            var J, $ = "_" == new Error(0, "_").fileName;
            if ("undefined" != typeof document && document.getElementsByTagName) {
                if (J = document.baseURI, !J) {
                    var N = document.getElementsByTagName("base");
                    J = N[0] && N[0].href || window.location.href;
                }
            }
            else
                "undefined" != typeof location && (J = e.location.href);
            if (J)
                J = J.split("#")[0].split("?")[0], J = J.substr(0, J.lastIndexOf("/") + 1);
            else {
                if ("undefined" == typeof process || !process.cwd)
                    throw new TypeError("No environment baseURI");
                J = "file://" + (D ? "/" : "") + process.cwd() + "/", D && (J = J.replace(/\\/g, "/"));
            }
            try {
                var B = "test:" == new e.URL("test:///").protocol;
            }
            catch (e) { }
            var H = B ? e.URL : e.URLPolyfill;
            q(r.prototype, "toString", { value: function () { return "Module"; } }), function () { function e(e) { return { status: "loading", name: e || "<Anonymous" + ++y + ">", linkSets: [], dependencies: [], metadata: {} }; } function a(e, t, r) { return new Promise(u({ step: r.address ? "fetch" : "locate", loader: e, moduleName: t, moduleMetadata: r && r.metadata || {}, moduleSource: r.source, moduleAddress: r.address })); } function o(t, r, n, a) { return new Promise(function (e, o) { e(t.loaderObj.normalize(r, n, a)); }).then(function (r) { var n; if (t.modules[r])
                return n = e(r), n.status = "linked", n.module = t.modules[r], n; for (var a = 0, o = t.loads.length; a < o; a++)
                if (n = t.loads[a], n.name == r)
                    return n; return n = e(r), t.loads.push(n), i(t, n), n; }); } function i(e, t) { s(e, t, Promise.resolve().then(function () { return e.loaderObj.locate({ name: t.name, metadata: t.metadata }); })); } function s(e, t, r) { l(e, t, r.then(function (r) { if ("loading" == t.status)
                return t.address = r, e.loaderObj.fetch({ name: t.name, metadata: t.metadata, address: r }); })); } function l(e, t, r) { r.then(function (r) { if ("loading" == t.status)
                return t.address = t.address || t.name, Promise.resolve(e.loaderObj.translate({ name: t.name, metadata: t.metadata, address: t.address, source: r })).then(function (r) { return t.source = r, e.loaderObj.instantiate({ name: t.name, metadata: t.metadata, address: t.address, source: r }); }).then(function (e) { if (void 0 === e)
                    throw new TypeError("Declarative modules unsupported in the polyfill."); if ("object" != typeof e)
                    throw new TypeError("Invalid instantiate return value"); t.depsList = e.deps || [], t.execute = e.execute; }).then(function () { t.dependencies = []; for (var r = t.depsList, n = [], a = 0, i = r.length; a < i; a++)
                    (function (r, a) { n.push(o(e, r, t.name, t.address).then(function (e) { if (t.dependencies[a] = { key: r, value: e.name }, "linked" != e.status)
                        for (var n = t.linkSets.concat([]), o = 0, i = n.length; o < i; o++)
                            c(n[o], e); })); })(r[a], a); return Promise.all(n); }).then(function () { t.status = "loaded"; for (var e = t.linkSets.concat([]), r = 0, n = e.length; r < n; r++)
                    m(e[r], t); }); }).catch(function (e) { t.status = "failed", t.exception = e; for (var r = t.linkSets.concat([]), n = 0, a = r.length; n < a; n++)
                p(r[n], t, e); }); } function u(t) { return function (r, n) { var a = t.loader, o = t.moduleName, u = t.step; if (a.modules[o])
                throw new TypeError('"' + o + '" already exists in the module table'); for (var c, f = 0, m = a.loads.length; f < m; f++)
                if (a.loads[f].name == o && (c = a.loads[f], "translate" != u || c.source || (c.address = t.moduleAddress, l(a, c, Promise.resolve(t.moduleSource))), c.linkSets.length && c.linkSets[0].loads[0].name == c.name))
                    return c.linkSets[0].done.then(function () { r(c); }); var p = c || e(o); p.metadata = t.moduleMetadata; var h = d(a, p); a.loads.push(p), r(h.done), "locate" == u ? i(a, p) : "fetch" == u ? s(a, p, Promise.resolve(t.moduleAddress)) : (p.address = t.moduleAddress, l(a, p, Promise.resolve(t.moduleSource))); }; } function d(e, t) { var r = { loader: e, loads: [], startingLoad: t, loadingCount: 0 }; return r.done = new Promise(function (e, t) { r.resolve = e, r.reject = t; }), c(r, t), r; } function c(e, t) { if ("failed" != t.status) {
                for (var r = 0, n = e.loads.length; r < n; r++)
                    if (e.loads[r] == t)
                        return;
                e.loads.push(t), t.linkSets.push(e), "loaded" != t.status && e.loadingCount++;
                for (var a = e.loader, r = 0, n = t.dependencies.length; r < n; r++)
                    if (t.dependencies[r]) {
                        var o = t.dependencies[r].value;
                        if (!a.modules[o])
                            for (var i = 0, s = a.loads.length; i < s; i++)
                                if (a.loads[i].name == o) {
                                    c(e, a.loads[i]);
                                    break;
                                }
                    }
            } } function f(e) { var t = !1; try {
                b(e, function (r, n) { p(e, r, n), t = !0; });
            }
            catch (r) {
                p(e, null, r), t = !0;
            } return t; } function m(e, t) { if (e.loadingCount--, !(e.loadingCount > 0)) {
                var r = e.startingLoad;
                if (e.loader.loaderObj.execute === !1) {
                    for (var n = [].concat(e.loads), a = 0, o = n.length; a < o; a++) {
                        var t = n[a];
                        t.module = { name: t.name, module: w({}), evaluated: !0 }, t.status = "linked", h(e.loader, t);
                    }
                    return e.resolve(r);
                }
                var i = f(e);
                i || e.resolve(r);
            } } function p(e, r, n) { var a = e.loader; e: if (r)
                if (e.loads[0].name == r.name)
                    n = t(n, "Error loading " + r.name);
                else {
                    for (var o = 0; o < e.loads.length; o++)
                        for (var i = e.loads[o], s = 0; s < i.dependencies.length; s++) {
                            var l = i.dependencies[s];
                            if (l.value == r.name) {
                                n = t(n, "Error loading " + r.name + ' as "' + l.key + '" from ' + i.name);
                                break e;
                            }
                        }
                    n = t(n, "Error loading " + r.name + " from " + e.loads[0].name);
                }
            else
                n = t(n, "Error linking " + e.loads[0].name); for (var u = e.loads.concat([]), o = 0, d = u.length; o < d; o++) {
                var r = u[o];
                a.loaderObj.failed = a.loaderObj.failed || [], U.call(a.loaderObj.failed, r) == -1 && a.loaderObj.failed.push(r);
                var c = U.call(r.linkSets, e);
                if (r.linkSets.splice(c, 1), 0 == r.linkSets.length) {
                    var f = U.call(e.loader.loads, r);
                    f != -1 && e.loader.loads.splice(f, 1);
                }
            } e.reject(n); } function h(e, t) { if (e.loaderObj.trace) {
                e.loaderObj.loads || (e.loaderObj.loads = {});
                var r = {};
                t.dependencies.forEach(function (e) { r[e.key] = e.value; }), e.loaderObj.loads[t.name] = { name: t.name, deps: t.dependencies.map(function (e) { return e.key; }), depMap: r, address: t.address, metadata: t.metadata, source: t.source };
            } t.name && (e.modules[t.name] = t.module); var n = U.call(e.loads, t); n != -1 && e.loads.splice(n, 1); for (var a = 0, o = t.linkSets.length; a < o; a++)
                n = U.call(t.linkSets[a].loads, t), n != -1 && t.linkSets[a].loads.splice(n, 1); t.linkSets.splice(0, t.linkSets.length); } function g(e, t, n) { try {
                var a = t.execute();
            }
            catch (e) {
                return void n(t, e);
            } return a && a instanceof r ? a : void n(t, new TypeError("Execution must define a Module instance")); } function v(e, t, r) { var n = e._loader.importPromises; return n[t] = r.then(function (e) { return n[t] = void 0, e; }, function (e) { throw n[t] = void 0, e; }); } function b(e, t) { var r = e.loader; if (e.loads.length)
                for (var n = e.loads.concat([]), a = 0; a < n.length; a++) {
                    var o = n[a], i = g(e, o, t);
                    if (!i)
                        return;
                    o.module = { name: o.name, module: i }, o.status = "linked", h(r, o);
                } } var y = 0; n.prototype = { constructor: n, define: function (e, t, r) { if (this._loader.importPromises[e])
                    throw new TypeError("Module is already loading."); return v(this, e, new Promise(u({ step: "translate", loader: this._loader, moduleName: e, moduleMetadata: r && r.metadata || {}, moduleSource: t, moduleAddress: r && r.address }))); }, delete: function (e) { var t = this._loader; return delete t.importPromises[e], delete t.moduleRecords[e], !!t.modules[e] && delete t.modules[e]; }, get: function (e) { if (this._loader.modules[e])
                    return this._loader.modules[e].module; }, has: function (e) { return !!this._loader.modules[e]; }, import: function (e, t, r) { "object" == typeof t && (t = t.name); var n = this; return Promise.resolve(n.normalize(e, t)).then(function (e) { var t = n._loader; return t.modules[e] ? t.modules[e].module : t.importPromises[e] || v(n, e, a(t, e, {}).then(function (r) { return delete t.importPromises[e], r.module.module; })); }); }, load: function (e) { var t = this._loader; return t.modules[e] ? Promise.resolve() : t.importPromises[e] || v(this, e, new Promise(u({ step: "locate", loader: t, moduleName: e, moduleMetadata: {}, moduleSource: void 0, moduleAddress: void 0 })).then(function () { delete t.importPromises[e]; })); }, module: function (t, r) { var n = e(); n.address = r && r.address; var a = d(this._loader, n), o = Promise.resolve(t), i = this._loader, s = a.done.then(function () { return n.module.module; }); return l(i, n, o), s; }, newModule: function (e) { if ("object" != typeof e)
                    throw new TypeError("Expected object"); var t = new r, n = []; if (Object.getOwnPropertyNames && null != e)
                    n = Object.getOwnPropertyNames(e);
                else
                    for (var a in e)
                        n.push(a); for (var o = 0; o < n.length; o++)
                    (function (r) { q(t, r, { configurable: !1, enumerable: !0, get: function () { return e[r]; }, set: function () { throw new Error("Module exports cannot be changed externally."); } }); })(n[o]); return Object.freeze && Object.freeze(t), t; }, set: function (e, t) { if (!(t instanceof r))
                    throw new TypeError("Loader.set(" + e + ", module) must be a module"); this._loader.modules[e] = { module: t }; }, normalize: function (e, t, r) { }, locate: function (e) { return e.name; }, fetch: function (e) { }, translate: function (e) { return e.source; }, instantiate: function (e) { } }; var w = n.prototype.newModule; }();
            var X, G;
            if ("undefined" != typeof XMLHttpRequest)
                G = function (e, t, r, n) { function a() { r(i.responseText); } function o() { n(new Error("XHR error" + (i.status ? " (" + i.status + (i.statusText ? " " + i.statusText : "") + ")" : "") + " loading " + e)); } var i = new XMLHttpRequest, s = !0, l = !1; if (!("withCredentials" in i)) {
                    var u = /^(\w+:)?\/\/([^\/]+)/.exec(e);
                    u && (s = u[2] === window.location.host, u[1] && (s &= u[1] === window.location.protocol));
                } s || "undefined" == typeof XDomainRequest || (i = new XDomainRequest, i.onload = a, i.onerror = o, i.ontimeout = o, i.onprogress = function () { }, i.timeout = 0, l = !0), i.onreadystatechange = function () { 4 === i.readyState && (0 == i.status ? i.responseText ? a() : (i.addEventListener("error", o), i.addEventListener("load", a)) : 200 === i.status ? a() : o()); }, i.open("GET", e, !0), i.setRequestHeader && (i.setRequestHeader("Accept", "application/x-es-module, */*"), t && ("string" == typeof t && i.setRequestHeader("Authorization", t), i.withCredentials = !0)), l ? setTimeout(function () { i.send(); }, 0) : i.send(null); };
            else if ("undefined" != typeof require && "undefined" != typeof process) {
                var Z;
                G = function (e, t, r, n) { if ("file:///" != e.substr(0, 8))
                    throw new Error('Unable to fetch "' + e + '". Only file URLs of the form file:/// allowed running in Node.'); return Z = Z || require("fs"), e = D ? e.replace(/\//g, "\\").substr(8) : e.substr(7), Z.readFile(e, function (e, t) { if (e)
                    return n(e); var a = t + ""; "\ufeff" === a[0] && (a = a.substr(1)), r(a); }); };
            }
            else {
                if ("undefined" == typeof self || "undefined" == typeof self.fetch)
                    throw new TypeError("No environment fetch API available.");
                G = function (e, t, r, n) { var a = { headers: { Accept: "application/x-es-module, */*" } }; t && ("string" == typeof t && (a.headers.Authorization = t), a.credentials = "include"), fetch(e, a).then(function (e) { if (e.ok)
                    return e.text(); throw new Error("Fetch error: " + e.status + " " + e.statusText); }).then(r, n); };
            }
            var W = function () { function t(t) { var n = this; return Promise.resolve(e["typescript" == n.transpiler ? "ts" : n.transpiler] || (n.pluginLoader || n).import(n.transpiler)).then(function (e) { e.__useDefault && (e = e.default); var a; return a = e.Compiler ? r : e.createLanguageService ? i : o, "(function(__moduleName){" + a.call(n, t, e) + '\n})("' + t.name + '");\n//# sourceURL=' + t.address + "!transpiled"; }); } function r(e, t) { var r = this.traceurOptions || {}; r.modules = "instantiate", r.script = !1, void 0 === r.sourceMaps && (r.sourceMaps = "inline"), r.filename = e.address, r.inputSourceMap = e.metadata.sourceMap, r.moduleName = !1; var n = new t.Compiler(r); return a(e.source, n, r.filename); } function a(e, t, r) { try {
                return t.compile(e, r);
            }
            catch (e) {
                if (e.length)
                    throw e[0];
                throw e;
            } } function o(e, t) { var r = this.babelOptions || {}; return r.modules = "system", void 0 === r.sourceMap && (r.sourceMap = "inline"), r.inputSourceMap = e.metadata.sourceMap, r.filename = e.address, r.code = !0, r.ast = !1, t.transform(e.source, r).code; } function i(e, t) { var r = this.typescriptOptions || {}; return r.target = r.target || t.ScriptTarget.ES5, void 0 === r.sourceMap && (r.sourceMap = !0), r.sourceMap && r.inlineSourceMap !== !1 && (r.inlineSourceMap = !0), r.module = t.ModuleKind.System, t.transpile(e.source, r, e.address); } return n.prototype.transpiler = "traceur", t; }();
            o.prototype = n.prototype, a.prototype = new o, a.prototype.constructor = a;
            var V, Y = /^[^\/]+:\/\//, K = new H(J), Q = !0;
            try {
                Object.getOwnPropertyDescriptor({ a: 0 }, "a");
            }
            catch (e) {
                Q = !1;
            }
            var ee;
            !function () { function r(e) { return l ? d + new Buffer(e).toString("base64") : "undefined" != typeof btoa ? d + btoa(unescape(encodeURIComponent(e))) : ""; } function n(e, t) { var n = e.source.lastIndexOf("\n"); "global" == e.metadata.format && (t = !1); var a = e.metadata.sourceMap; if (a) {
                if ("object" != typeof a)
                    throw new TypeError("load.metadata.sourceMap must be set to an object.");
                a = JSON.stringify(a);
            } return (t ? "(function(System, SystemJS) {" : "") + e.source + (t ? "\n})(System, System);" : "") + ("\n//# sourceURL=" != e.source.substr(n, 15) ? "\n//# sourceURL=" + e.address + (a ? "!transpiled" : "") : "") + (a && r(a) || ""); } function a(t, r) { u = r, 0 == p++ && (c = e.System), e.System = e.SystemJS = t; } function o() { 0 == --p && (e.System = e.SystemJS = c), u = void 0; } function s(e) { g || (g = document.head || document.body || document.documentElement); var r = document.createElement("script"); r.text = n(e, !1); var i, s = window.onerror; if (window.onerror = function (r) { i = t(r, "Evaluating " + e.address), s && s.apply(this, arguments); }, a(this, e), e.metadata.integrity && r.setAttribute("integrity", e.metadata.integrity), e.metadata.nonce && r.setAttribute("nonce", e.metadata.nonce), g.appendChild(r), g.removeChild(r), o(), window.onerror = s, i)
                throw i; } var l = "undefined" != typeof Buffer; try {
                l && "YQ==" != new Buffer("a").toString("base64") && (l = !1);
            }
            catch (e) {
                l = !1;
            } var u, d = "\n//# sourceMappingURL=data:application/json;base64,"; i("pushRegister_", function () { return function (e) { return !!u && (this.reduceRegister_(u, e), !0); }; }); var c, f, m, p = 0; ee = function (e) { if (e.source) {
                if ((e.metadata.integrity || e.metadata.nonce) && h)
                    return s.call(this, e);
                try {
                    a(this, e), u = e, !m && this._nodeRequire && (m = this._nodeRequire("vm"), f = m.runInThisContext("typeof System !== 'undefined' && System") === this), f ? m.runInThisContext(n(e, !0), { filename: e.address + (e.metadata.sourceMap ? "!transpiled" : "") }) : (0, eval)(n(e, !0)), o();
                }
                catch (r) {
                    throw o(), t(r, "Evaluating " + e.address);
                }
            } }; var h = !1; F && "undefined" != typeof document && document.getElementsByTagName && (window.chrome && window.chrome.extension || navigator.userAgent.match(/^Node\.js/) || (h = !0)); var g; }();
            var te;
            s(function (e) { return function () { e.call(this), this.baseURL = J, this.map = {}, "undefined" != typeof $__curScript && (this.scriptSrc = $__curScript.src), this.warnings = !1, this.defaultJSExtensions = !1, this.pluginFirst = !1, this.loaderErrorStack = !1, this.set("@empty", this.newModule({})), E.call(this, !1, !1); }; }), "undefined" == typeof require || "undefined" == typeof process || process.browser || (a.prototype._nodeRequire = require);
            var re;
            i("normalize", function (e) { return function (e, t, r) { var n = P.call(this, e, t); return !this.defaultJSExtensions || r || ".js" == n.substr(n.length - 3, 3) || d(n) || (n += ".js"), n; }; });
            var ne = "undefined" != typeof XMLHttpRequest;
            i("locate", function (e) { return function (t) { return Promise.resolve(e.call(this, t)).then(function (e) { return ne ? e.replace(/#/g, "%23") : e; }); }; }), i("fetch", function () { return function (e) { return new Promise(function (t, r) { G(e.address, e.metadata.authorization, t, r); }); }; }), i("import", function (e) { return function (t, r, n) { return r && r.name && w.call(this, "SystemJS.import(name, { name: parentName }) is deprecated for SystemJS.import(name, parentName), while importing " + t + " from " + r.name), e.call(this, t, r, n).then(function (e) { return e.__useDefault ? e.default : e; }); }; }), i("translate", function (e) { return function (t) { return "detect" == t.metadata.format && (t.metadata.format = void 0), e.apply(this, arguments); }; }), i("instantiate", function (e) { return function (e) { if ("json" == e.metadata.format && !this.builder) {
                var t = e.metadata.entry = M();
                t.deps = [], t.execute = function () { try {
                    return JSON.parse(e.source);
                }
                catch (t) {
                    throw new Error("Invalid JSON file " + e.name);
                } };
            } }; }), a.prototype.getConfig = function (e) { var t = {}, r = this; for (var n in r)
                r.hasOwnProperty && !r.hasOwnProperty(n) || n in a.prototype && "transpiler" != n || U.call(["_loader", "amdDefine", "amdRequire", "defined", "failed", "version", "loads"], n) == -1 && (t[n] = r[n]); return t.production = te.production, t; };
            var ae;
            a.prototype.config = function (e, t) { function r(e) { for (var t in e)
                if (e.hasOwnProperty(t))
                    return !0; } var n = this; if ("loaderErrorStack" in e && (ae = $__curScript, e.loaderErrorStack ? $__curScript = void 0 : $__curScript = ae), "warnings" in e && (n.warnings = e.warnings), e.transpilerRuntime === !1 && (n._loader.loadedTranspilerRuntime = !0), ("production" in e || "build" in e) && E.call(n, !!e.production, !!(e.build || te && te.build)), !t) {
                var a;
                if (O(n, e, function (e) { a = a || e.baseURL; }), a = a || e.baseURL) {
                    if (r(n.packages) || r(n.meta) || r(n.depCache) || r(n.bundles) || r(n.packageConfigPaths))
                        throw new TypeError("Incorrect configuration order. The baseURL must be configured with the first SystemJS.config call.");
                    this.baseURL = a, _.call(this);
                }
                if (e.paths && g(n.paths, e.paths), O(n, e, function (e) { e.paths && g(n.paths, e.paths); }), this.warnings)
                    for (var o in n.paths)
                        o.indexOf("*") != -1 && w.call(n, 'Paths configuration "' + o + '" -> "' + n.paths[o] + '" uses wildcards which are being deprecated for simpler trailing "/" folder paths.');
            } if (e.defaultJSExtensions && (n.defaultJSExtensions = e.defaultJSExtensions, w.call(n, "The defaultJSExtensions configuration option is deprecated, use packages configuration instead.")), e.pluginFirst && (n.pluginFirst = e.pluginFirst), e.map) {
                var i = "";
                for (var o in e.map) {
                    var s = e.map[o];
                    if ("string" != typeof s) {
                        i += (i.length ? ", " : "") + '"' + o + '"';
                        var l = n.defaultJSExtensions && ".js" != o.substr(o.length - 3, 3), u = n.decanonicalize(o);
                        l && ".js" == u.substr(u.length - 3, 3) && (u = u.substr(0, u.length - 3));
                        var c = "";
                        for (var f in n.packages)
                            u.substr(0, f.length) == f && (!u[f.length] || "/" == u[f.length]) && c.split("/").length < f.split("/").length && (c = f);
                        c && n.packages[c].main && (u = u.substr(0, u.length - n.packages[c].main.length - 1));
                        var f = n.packages[u] = n.packages[u] || {};
                        f.map = s;
                    }
                    else
                        n.map[o] = s;
                }
                i && w.call(n, "The map configuration for " + i + ' uses object submaps, which is deprecated in global map.\nUpdate this to use package contextual map with configs like SystemJS.config({ packages: { "' + o + '": { map: {...} } } }).');
            } if (e.packageConfigPaths) {
                for (var m = [], p = 0; p < e.packageConfigPaths.length; p++) {
                    var h = e.packageConfigPaths[p], v = Math.max(h.lastIndexOf("*") + 1, h.lastIndexOf("/")), b = P.call(n, h.substr(0, v));
                    m[p] = b + h.substr(v);
                }
                n.packageConfigPaths = m;
            } if (e.bundles)
                for (var o in e.bundles) {
                    for (var x = [], p = 0; p < e.bundles[o].length; p++) {
                        var l = n.defaultJSExtensions && ".js" != e.bundles[o][p].substr(e.bundles[o][p].length - 3, 3), S = n.decanonicalize(e.bundles[o][p]);
                        l && ".js" == S.substr(S.length - 3, 3) && (S = S.substr(0, S.length - 3)), x.push(S);
                    }
                    n.bundles[o] = x;
                } if (e.packages)
                for (var o in e.packages) {
                    if (o.match(/^([^\/]+:)?\/\/$/))
                        throw new TypeError('"' + o + '" is not a valid package name.');
                    var u = P.call(n, o);
                    "/" == u[u.length - 1] && (u = u.substr(0, u.length - 1)), y(n, u, e.packages[o], !1);
                } for (var j in e) {
                var s = e[j];
                if (U.call(["baseURL", "map", "packages", "bundles", "paths", "warnings", "packageConfigPaths", "loaderErrorStack", "browserConfig", "nodeConfig", "devConfig", "buildConfig", "productionConfig"], j) == -1)
                    if ("object" != typeof s || s instanceof Array)
                        n[j] = s;
                    else {
                        n[j] = n[j] || {};
                        for (var o in s)
                            if ("meta" == j && "*" == o[0])
                                g(n[j][o] = n[j][o] || {}, s[o]);
                            else if ("meta" == j) {
                                var k = P.call(n, o);
                                n.defaultJSExtensions && ".js" != k.substr(k.length - 3, 3) && !d(k) && (k += ".js"), g(n[j][k] = n[j][k] || {}, s[o]);
                            }
                            else if ("depCache" == j) {
                                var l = n.defaultJSExtensions && ".js" != o.substr(o.length - 3, 3), u = n.decanonicalize(o);
                                l && ".js" == u.substr(u.length - 3, 3) && (u = u.substr(0, u.length - 3)), n[j][u] = [].concat(s[o]);
                            }
                            else
                                n[j][o] = s[o];
                    }
            } O(n, e, function (e) { n.config(e, !0); }); }, function () {
                function e(e, t) { var r, n, a = 0; for (var o in e.packages)
                    t.substr(0, o.length) !== o || t.length !== o.length && "/" !== t[o.length] || (n = o.split("/").length, n > a && (r = o, a = n)); return r; }
                function t(e, t, r, n, a) { if (!n || "/" == n[n.length - 1] || a || t.defaultExtension === !1)
                    return n; var o = !1; if (t.meta && p(t.meta, n, function (e, t, r) { if (0 == r || e.lastIndexOf("*") != e.length - 1)
                    return o = !0; }), !o && e.meta && p(e.meta, r + "/" + n, function (e, t, r) { if (0 == r || e.lastIndexOf("*") != e.length - 1)
                    return o = !0; }), o)
                    return n; var i = "." + (t.defaultExtension || "js"); return n.substr(n.length - i.length) != i ? n + i : n; }
                function r(e, r, n, a, i) { if (!a) {
                    if (!r.main)
                        return n + (e.defaultJSExtensions ? ".js" : "");
                    a = "./" == r.main.substr(0, 2) ? r.main.substr(2) : r.main;
                } if (r.map) {
                    var s = "./" + a, l = S(r.map, s);
                    if (l || (s = "./" + t(e, r, n, a, i), s != "./" + a && (l = S(r.map, s))), l) {
                        var u = o(e, r, n, l, s, i);
                        if (u)
                            return u;
                    }
                } return n + "/" + t(e, r, n, a, i); }
                function n(e, t, r, n) { if ("." == e)
                    throw new Error("Package " + r + ' has a map entry for "." which is not permitted.'); return !(t.substr(0, e.length) == e && n.length > e.length); }
                function o(e, r, a, o, i, s) { "/" == i[i.length - 1] && (i = i.substr(0, i.length - 1)); var l = r.map[o]; if ("object" == typeof l)
                    throw new Error("Synchronous conditional normalization not supported sync normalizing " + o + " in " + a); if (n(o, l, a, i) && "string" == typeof l) {
                    if ("." == l)
                        l = a;
                    else if ("./" == l.substr(0, 2))
                        return a + "/" + t(e, r, a, l.substr(2) + i.substr(o.length), s);
                    return e.normalizeSync(l + i.substr(o.length), a + "/");
                } }
                function l(e, r, n, a, o) { if (!a) {
                    if (!r.main)
                        return Promise.resolve(n + (e.defaultJSExtensions ? ".js" : ""));
                    a = "./" == r.main.substr(0, 2) ? r.main.substr(2) : r.main;
                } var i, s; return r.map && (i = "./" + a, s = S(r.map, i), s || (i = "./" + t(e, r, n, a, o), i != "./" + a && (s = S(r.map, i)))), (s ? d(e, r, n, s, i, o) : Promise.resolve()).then(function (i) { return i ? Promise.resolve(i) : Promise.resolve(n + "/" + t(e, r, n, a, o)); }); }
                function u(e, r, n, a, o, i, s) { if ("." == o)
                    o = n;
                else if ("./" == o.substr(0, 2))
                    return Promise.resolve(n + "/" + t(e, r, n, o.substr(2) + i.substr(a.length), s)).then(function (t) { return L.call(e, t, n + "/"); }); return e.normalize(o + i.substr(a.length), n + "/"); }
                function d(e, t, r, a, o, i) { "/" == o[o.length - 1] && (o = o.substr(0, o.length - 1)); var s = t.map[a]; if ("string" == typeof s)
                    return n(a, s, r, o) ? u(e, t, r, a, s, o, i) : Promise.resolve(); if (e.builder)
                    return Promise.resolve(r + "/#:" + o); var l = [], d = []; for (var c in s) {
                    var f = z(c);
                    d.push({ condition: f, map: s[c] }), l.push(e.import(f.module, r));
                } return Promise.all(l).then(function (e) { for (var t = 0; t < d.length; t++) {
                    var r = d[t].condition, n = x(r.prop, e[t]);
                    if (!r.negate && n || r.negate && !n)
                        return d[t].map;
                } }).then(function (s) { if (s) {
                    if (!n(a, s, r, o))
                        return;
                    return u(e, t, r, a, s, o, i);
                } }); }
                function c(e) { var t = e.lastIndexOf("*"), r = Math.max(t + 1, e.lastIndexOf("/")); return { length: r, regEx: new RegExp("^(" + e.substr(0, r).replace(/[.+?^${}()|[\]\\]/g, "\\$&").replace(/\*/g, "[^\\/]+") + ")(\\/|$)"), wildcard: t != -1 }; }
                function f(e, t) { for (var r, n, a = !1, o = 0; o < e.packageConfigPaths.length; o++) {
                    var i = e.packageConfigPaths[o], s = h[i] || (h[i] = c(i));
                    if (!(t.length < s.length)) {
                        var l = t.match(s.regEx);
                        !l || r && (a && s.wildcard || !(r.length < l[1].length)) || (r = l[1], a = !s.wildcard, n = r + i.substr(s.length));
                    }
                } if (r)
                    return { packageName: r, configPath: n }; }
                function m(e, t, r) { var n = e.pluginLoader || e; return (n.meta[r] = n.meta[r] || {}).format = "json", n.meta[r].loader = null, n.load(r).then(function () { var a = n.get(r).default; return a.systemjs && (a = a.systemjs), a.modules && (a.meta = a.modules, w.call(e, "Package config file " + r + ' is configured with "modules", which is deprecated as it has been renamed to "meta".')), y(e, t, a, !0); }); }
                function p(e, t, r) { var n; for (var a in e) {
                    var o = "./" == a.substr(0, 2) ? "./" : "";
                    if (o && (a = a.substr(2)), n = a.indexOf("*"), n !== -1 && a.substr(0, n) == t.substr(0, n) && a.substr(n + 1) == t.substr(t.length - a.length + n + 1) && r(a, e[o + a], a.split("/").length))
                        return;
                } var i = e[t] && e.hasOwnProperty && e.hasOwnProperty(t) ? e[t] : e["./" + t]; i && r(i, i, 0); }
                s(function (e) { return function () { e.call(this), this.packages = {}, this.packageConfigPaths = []; }; }), a.prototype.normalizeSync = a.prototype.decanonicalize = a.prototype.normalize, i("decanonicalize", function (t) { return function (r, n) { if (this.builder)
                    return t.call(this, r, n, !0); var a = t.call(this, r, n, !1); if (!this.defaultJSExtensions)
                    return a; var o = e(this, a), i = this.packages[o], s = i && i.defaultExtension; return void 0 == s && i && i.meta && p(i.meta, a.substr(o), function (e, t, r) { if (0 == r || e.lastIndexOf("*") != e.length - 1)
                    return s = !1, !0; }), (s === !1 || s && ".js" != s) && ".js" != r.substr(r.length - 3, 3) && ".js" == a.substr(a.length - 3, 3) && (a = a.substr(0, a.length - 3)), a; }; }), i("normalizeSync", function (t) {
                    return function (n, a, i) {
                        var s = this;
                        if (i = i === !0, a)
                            var l = e(s, a) || s.defaultJSExtensions && ".js" == a.substr(a.length - 3, 3) && e(s, a.substr(0, a.length - 3));
                        var u = l && s.packages[l];
                        if (u && "." != n[0]) {
                            var d = u.map, c = d && S(d, n);
                            if (c && "string" == typeof d[c]) {
                                var m = o(s, u, l, c, n, i);
                                if (m)
                                    return m;
                            }
                        }
                        var p = s.defaultJSExtensions && ".js" != n.substr(n.length - 3, 3), h = t.call(s, n, a, !1);
                        p && ".js" != h.substr(h.length - 3, 3) && (p = !1), p && (h = h.substr(0, h.length - 3));
                        var g = f(s, h), v = g && g.packageName || e(s, h);
                        if (!v)
                            return h + (p ? ".js" : "");
                        var b = h.substr(v.length + 1);
                        return r(s, s.packages[v] || {}, v, b, i);
                    };
                }), i("normalize", function (t) { return function (r, n, a) { var o = this; return a = a === !0, Promise.resolve().then(function () { if (n)
                    var t = e(o, n) || o.defaultJSExtensions && ".js" == n.substr(n.length - 3, 3) && e(o, n.substr(0, n.length - 3)); var i = t && o.packages[t]; if (i && "./" != r.substr(0, 2)) {
                    var s = i.map, l = s && S(s, r);
                    if (l)
                        return d(o, i, t, l, r, a);
                } return Promise.resolve(); }).then(function (i) { if (i)
                    return i; var s = o.defaultJSExtensions && ".js" != r.substr(r.length - 3, 3), u = t.call(o, r, n, !1); s && ".js" != u.substr(u.length - 3, 3) && (s = !1), s && (u = u.substr(0, u.length - 3)); var d = f(o, u), c = d && d.packageName || e(o, u); if (!c)
                    return Promise.resolve(u + (s ? ".js" : "")); var p = o.packages[c], h = p && (p.configured || !d); return (h ? Promise.resolve(p) : m(o, c, d.configPath)).then(function (e) { var t = u.substr(c.length + 1); return l(o, e, c, t, a); }); }); }; });
                var h = {};
                i("locate", function (t) { return function (r) { var n = this; return Promise.resolve(t.call(this, r)).then(function (t) { var a = e(n, r.name); if (a) {
                    var o = n.packages[a], i = r.name.substr(a.length + 1), s = {};
                    if (o.meta) {
                        var l = 0;
                        p(o.meta, i, function (e, t, r) { r > l && (l = r), v(s, t, r && l > r); }), v(r.metadata, s);
                    }
                    o.format && !r.metadata.loader && (r.metadata.format = r.metadata.format || o.format);
                } return t; }); }; });
            }(), function () { function t() { if (s && "interactive" === s.script.readyState)
                return s.load; for (var e = 0; e < d.length; e++)
                if ("interactive" == d[e].script.readyState)
                    return s = d[e], s.load; } function r(e, t) { return new Promise(function (e, r) { t.metadata.integrity && r(new Error("Subresource integrity checking is not supported in web workers.")), l = t; try {
                importScripts(t.address);
            }
            catch (e) {
                l = null, r(e);
            } l = null, t.metadata.entry || r(new Error(t.address + " did not call System.register or AMD define. If loading a global, ensure the meta format is set to global.")), e(""); }); } if ("undefined" != typeof document)
                var n = document.getElementsByTagName("head")[0]; var a, o, s, l = null, u = n && function () { var e = document.createElement("script"), t = "undefined" != typeof opera && "[object Opera]" === opera.toString(); return e.attachEvent && !(e.attachEvent.toString && e.attachEvent.toString().indexOf("[native code") < 0) && !t; }(), d = [], c = 0, f = []; i("pushRegister_", function (e) { return function (r) { return !e.call(this, r) && (l ? this.reduceRegister_(l, r) : u ? this.reduceRegister_(t(), r) : c ? f.push(r) : this.reduceRegister_(null, r), !0); }; }), i("fetch", function (t) { return function (i) { var l = this; return "json" != i.metadata.format && i.metadata.scriptLoad && (F || A) ? A ? r(l, i) : new Promise(function (t, r) { function m(e) { if (!g.readyState || "loaded" == g.readyState || "complete" == g.readyState) {
                if (c--, i.metadata.entry || f.length) {
                    if (!u) {
                        for (var n = 0; n < f.length; n++)
                            l.reduceRegister_(i, f[n]);
                        f = [];
                    }
                }
                else
                    l.reduceRegister_(i);
                h(), i.metadata.entry || i.metadata.bundle || r(new Error(i.name + " did not call System.register or AMD define. If loading a global module configure the global name via the meta exports property for script injection support.")), t("");
            } } function p(e) { h(), r(new Error("Unable to load script " + i.address)); } function h() { if (e.System = a, e.require = o, g.detachEvent) {
                g.detachEvent("onreadystatechange", m);
                for (var t = 0; t < d.length; t++)
                    d[t].script == g && (s && s.script == g && (s = null), d.splice(t, 1));
            }
            else
                g.removeEventListener("load", m, !1), g.removeEventListener("error", p, !1); n.removeChild(g); } var g = document.createElement("script"); g.async = !0, i.metadata.crossOrigin && (g.crossOrigin = i.metadata.crossOrigin), i.metadata.integrity && g.setAttribute("integrity", i.metadata.integrity), u ? (g.attachEvent("onreadystatechange", m), d.push({ script: g, load: i })) : (g.addEventListener("load", m, !1), g.addEventListener("error", p, !1)), c++, a = e.System, o = e.require, g.src = i.address, n.appendChild(g); }) : t.call(this, i); }; }); }();
            var oe = /^(\s*\/\*[^\*]*(\*(?!\/)[^\*]*)*\*\/|\s*\/\/[^\n]*|\s*"[^"]+"\s*;?|\s*'[^']+'\s*;?)*\s*/;
            !function () { function t(e, r, n) { if (n[e.groupIndex] = n[e.groupIndex] || [], U.call(n[e.groupIndex], e) == -1) {
                n[e.groupIndex].push(e);
                for (var a = 0, o = e.normalizedDeps.length; a < o; a++) {
                    var i = e.normalizedDeps[a], s = r.defined[i];
                    if (s && !s.evaluated) {
                        var l = e.groupIndex + (s.declarative != e.declarative);
                        if (null === s.groupIndex || s.groupIndex < l) {
                            if (null !== s.groupIndex && (n[s.groupIndex].splice(U.call(n[s.groupIndex], s), 1), 0 == n[s.groupIndex].length))
                                throw new Error("Mixed dependency cycle detected");
                            s.groupIndex = l;
                        }
                        t(s, r, n);
                    }
                }
            } } function n(e, r, n) { if (!r.module) {
                r.groupIndex = 0;
                var a = [];
                t(r, n, a);
                for (var o = !!r.declarative == a.length % 2, i = a.length - 1; i >= 0; i--) {
                    for (var s = a[i], l = 0; l < s.length; l++) {
                        var d = s[l];
                        o ? u(d, n) : c(d, n);
                    }
                    o = !o;
                }
            } } function o() { } function l(e, t) { return t[e] || (t[e] = { name: e, dependencies: [], exports: new o, importers: [] }); } function u(t, r) { if (!t.module) {
                var n = r._loader.moduleRecords, a = t.module = l(t.name, n), o = t.module.exports, i = t.declare.call(e, function (e, t) { if (a.locked = !0, "object" == typeof e)
                    for (var r in e)
                        o[r] = e[r];
                else
                    o[e] = t; for (var n = 0, i = a.importers.length; n < i; n++) {
                    var s = a.importers[n];
                    if (!s.locked) {
                        var l = U.call(s.dependencies, a), u = s.setters[l];
                        u && u(o);
                    }
                } return a.locked = !1, t; }, { id: t.name });
                if ("function" == typeof i && (i = { setters: [], execute: i }), i = i || { setters: [], execute: function () { } }, a.setters = i.setters, a.execute = i.execute, !a.setters || !a.execute)
                    throw new TypeError("Invalid System.register form for " + t.name);
                for (var s = 0, d = t.normalizedDeps.length; s < d; s++) {
                    var c, f = t.normalizedDeps[s], m = r.defined[f], p = n[f];
                    p ? c = p.exports : m && !m.declarative ? c = m.esModule : m ? (u(m, r), p = m.module, c = p.exports) : c = r.get(f), p && p.importers ? (p.importers.push(a), a.dependencies.push(p)) : a.dependencies.push(null);
                    for (var h = t.originalIndices[s], g = 0, v = h.length; g < v; ++g) {
                        var b = h[g];
                        a.setters[b] && a.setters[b](c);
                    }
                }
            } } function d(e, t) { var r, n = t.defined[e]; if (n)
                n.declarative ? f(e, n, [], t) : n.evaluated || c(n, t), r = n.module.exports;
            else if (r = t.get(e), !r)
                throw new Error("Unable to load dependency " + e + "."); return (!n || n.declarative) && r && r.__useDefault ? r.default : r; } function c(t, n) { if (!t.module) {
                var a = {}, o = t.module = { exports: a, id: t.name };
                if (!t.executingRequire)
                    for (var i = 0, s = t.normalizedDeps.length; i < s; i++) {
                        var l = t.normalizedDeps[i], u = n.defined[l];
                        u && c(u, n);
                    }
                t.evaluated = !0;
                var f = t.execute.call(e, function (e) { for (var r = 0, a = t.deps.length; r < a; r++)
                    if (t.deps[r] == e)
                        return d(t.normalizedDeps[r], n); var o = n.normalizeSync(e, t.name); if (U.call(t.normalizedDeps, o) != -1)
                    return d(o, n); throw new Error("Module " + e + " not declared as a dependency of " + t.name); }, a, o);
                void 0 !== f && (o.exports = f), a = o.exports, a && (a.__esModule || a instanceof r) ? t.esModule = n.newModule(a) : t.esmExports && a !== e ? t.esModule = n.newModule(p(a)) : t.esModule = n.newModule({ default: a, __useDefault: !0 });
            } } function f(t, r, n, a) { if (r && !r.evaluated && r.declarative) {
                n.push(t);
                for (var o = 0, i = r.normalizedDeps.length; o < i; o++) {
                    var s = r.normalizedDeps[o];
                    U.call(n, s) == -1 && (a.defined[s] ? f(s, a.defined[s], n, a) : a.get(s));
                }
                r.evaluated || (r.evaluated = !0, r.module.execute.call(e));
            } } a.prototype.register = function (e, t, r) { if ("string" != typeof e && (r = t, t = e, e = null), "boolean" == typeof r)
                return this.registerDynamic.apply(this, arguments); var n = M(); n.name = e && (this.decanonicalize || this.normalize).call(this, e), n.declarative = !0, n.deps = t, n.declare = r, this.pushRegister_({ amd: !1, entry: n }); }, a.prototype.registerDynamic = function (e, t, r, n) { "string" != typeof e && (n = r, r = t, t = e, e = null); var a = M(); a.name = e && (this.decanonicalize || this.normalize).call(this, e), a.deps = t, a.execute = n, a.executingRequire = r, this.pushRegister_({ amd: !1, entry: a }); }, i("reduceRegister_", function () { return function (e, t) { if (t) {
                var r = t.entry, n = e && e.metadata;
                if (r.name && (r.name in this.defined || (this.defined[r.name] = r), n && (n.bundle = !0)), !r.name || e && !n.entry && r.name == e.name) {
                    if (!n)
                        throw new TypeError("Invalid System.register call. Anonymous System.register calls can only be made by modules loaded by SystemJS.import and not via script tags.");
                    if (n.entry)
                        throw "register" == n.format ? new Error("Multiple anonymous System.register calls in module " + e.name + ". If loading a bundle, ensure all the System.register calls are named.") : new Error("Module " + e.name + " interpreted as " + n.format + " module format, but called System.register.");
                    n.format || (n.format = "register"), n.entry = r;
                }
            } }; }), s(function (e) { return function () { e.call(this), this.defined = {}, this._loader.moduleRecords = {}; }; }), q(o, "toString", { value: function () { return "Module"; } }), i("delete", function (e) { return function (t) { return delete this._loader.moduleRecords[t], delete this.defined[t], e.call(this, t); }; }), i("fetch", function (e) { return function (t) { return this.defined[t.name] ? (t.metadata.format = "defined", "") : (t.metadata.deps = t.metadata.deps || [], e.call(this, t)); }; }), i("translate", function (e) { return function (t) { return t.metadata.deps = t.metadata.deps || [], Promise.resolve(e.apply(this, arguments)).then(function (e) { return ("register" == t.metadata.format || !t.metadata.format && k(t.source)) && (t.metadata.format = "register"), e; }); }; }), i("load", function (e) { return function (t) { var r = this, a = r.defined[t]; return !a || a.deps.length ? e.apply(this, arguments) : (a.originalIndices = a.normalizedDeps = [], n(t, a, r), f(t, a, [], r), a.esModule || (a.esModule = r.newModule(a.module.exports)), r.trace || (r.defined[t] = void 0), r.set(t, a.esModule), Promise.resolve()); }; }), i("instantiate", function (e) { return function (t) { "detect" == t.metadata.format && (t.metadata.format = void 0), e.call(this, t); var r, a = this; if (a.defined[t.name])
                r = a.defined[t.name], r.declarative || (r.deps = r.deps.concat(t.metadata.deps)), r.deps = r.deps.concat(t.metadata.deps);
            else if (t.metadata.entry)
                r = t.metadata.entry, r.deps = r.deps.concat(t.metadata.deps);
            else if (!(a.builder && t.metadata.bundle || "register" != t.metadata.format && "esm" != t.metadata.format && "es6" != t.metadata.format)) {
                if ("undefined" != typeof ee && ee.call(a, t), !t.metadata.entry && !t.metadata.bundle)
                    throw new Error(t.name + " detected as " + t.metadata.format + " but didn't execute.");
                r = t.metadata.entry, r && t.metadata.deps && (r.deps = r.deps.concat(t.metadata.deps));
            } r || (r = M(), r.deps = t.metadata.deps, r.execute = function () { }), a.defined[t.name] = r; var o = m(r.deps); r.deps = o.names, r.originalIndices = o.indices, r.name = t.name, r.esmExports = t.metadata.esmExports !== !1; for (var i = [], s = 0, l = r.deps.length; s < l; s++)
                i.push(Promise.resolve(a.normalize(r.deps[s], t.name))); return Promise.all(i).then(function (e) { return r.normalizedDeps = e, { deps: r.deps, execute: function () { return n(t.name, r, a), f(t.name, r, [], a), r.esModule || (r.esModule = a.newModule(r.module.exports)), a.trace || (a.defined[t.name] = void 0), r.esModule; } }; }); }; }); }(), function () { var r = /(^\s*|[}\);\n]\s*)(import\s*(['"]|(\*\s+as\s+)?[^"'\(\)\n;]+\s*from\s*['"]|\{)|export\s+\*\s+from\s+["']|export\s*(\{|default|function|class|var|const|let|async\s+function))/, n = /\$traceurRuntime\s*\./, a = /babelHelpers\s*\./; i("translate", function (o) { return function (i) { var s = this, l = arguments; return o.apply(s, l).then(function (o) { if ("esm" == i.metadata.format || "es6" == i.metadata.format || !i.metadata.format && o.match(r)) {
                if ("es6" == i.metadata.format && w.call(s, "Module " + i.name + ' has metadata setting its format to "es6", which is deprecated.\nThis should be updated to "esm".'), i.metadata.format = "esm", i.metadata.deps) {
                    for (var u = "", d = 0; d < i.metadata.deps.length; d++)
                        u += 'import "' + i.metadata.deps[d] + '"; ';
                    i.source = u + o;
                }
                if (s.transpiler === !1) {
                    if (s.builder)
                        return o;
                    throw new TypeError("Unable to dynamically transpile ES module as SystemJS.transpiler set to false.");
                }
                return s._loader.loadedTranspiler = s._loader.loadedTranspiler || !1, s.pluginLoader && (s.pluginLoader._loader.loadedTranspiler = s._loader.loadedTranspiler || !1), (s._loader.transpilerPromise || (s._loader.transpilerPromise = Promise.resolve(e["typescript" == s.transpiler ? "ts" : s.transpiler] || (s.pluginLoader || s).normalize(s.transpiler).then(function (e) { return s._loader.transpilerNormalized = e, (s.pluginLoader || s).load(e).then(function () { return (s.pluginLoader || s).get(e); }); })))).then(function (e) { return s._loader.loadedTranspilerRuntime = !0, e.translate ? e == i.metadata.loaderModule ? i.source : (i.metadata.loaderModule = e, i.metadata.loader = s._loader.transpilerNormalized, "string" == typeof i.metadata.sourceMap && (i.metadata.sourceMap = JSON.parse(i.metadata.sourceMap)), Promise.resolve(e.translate.apply(s, l)).then(function (e) { var t = i.metadata.sourceMap; if (t && "object" == typeof t) {
                    var r = i.address.split("!")[0];
                    t.file && t.file != i.address || (t.file = r + "!transpiled"), (!t.sources || t.sources.length <= 1 && (!t.sources[0] || t.sources[0] == i.address)) && (t.sources = [r]);
                } return "esm" == i.metadata.format && !s.builder && k(e) && (i.metadata.format = "register"), e; })) : (s.builder && (i.metadata.originalSource = i.source), W.call(s, i).then(function (e) { return i.metadata.sourceMap = void 0, e; })); }, function (e) { throw t(e, "Unable to load transpiler to transpile " + i.name); });
            } if (s.transpiler === !1)
                return o; if (s._loader.loadedTranspiler !== !1 || "traceur" != s.transpiler && "typescript" != s.transpiler && "babel" != s.transpiler || i.name != s.normalizeSync(s.transpiler) || (o.length > 100 && !i.metadata.format && (i.metadata.format = "global", "traceur" === s.transpiler && (i.metadata.exports = "traceur"), "typescript" === s.transpiler && (i.metadata.exports = "ts")), s._loader.loadedTranspiler = !0), s._loader.loadedTranspilerRuntime === !1 && (i.name != s.normalizeSync("traceur-runtime") && i.name != s.normalizeSync("babel/external-helpers*") || (o.length > 100 && (i.metadata.format = i.metadata.format || "global"), s._loader.loadedTranspilerRuntime = !0)), ("register" == i.metadata.format || i.metadata.bundle) && s._loader.loadedTranspilerRuntime !== !0) {
                if ("traceur" == s.transpiler && !e.$traceurRuntime && i.source.match(n))
                    return s._loader.loadedTranspilerRuntime = s._loader.loadedTranspilerRuntime || !1, s.import("traceur-runtime").then(function () { return o; });
                if ("babel" == s.transpiler && !e.babelHelpers && i.source.match(a))
                    return s._loader.loadedTranspilerRuntime = s._loader.loadedTranspilerRuntime || !1, s.import("babel/external-helpers").then(function () { return o; });
            } return o; }); }; }); }();
            var ie = "undefined" != typeof self ? "self" : "global";
            i("fetch", function (e) { return function (t) { return t.metadata.exports && !t.metadata.format && (t.metadata.format = "global"), e.call(this, t); }; }), i("instantiate", function (e) { return function (t) { var r = this; if (t.metadata.format || (t.metadata.format = "global"), "global" == t.metadata.format && !t.metadata.entry) {
                var n = M();
                t.metadata.entry = n, n.deps = [];
                for (var a in t.metadata.globals) {
                    var o = t.metadata.globals[a];
                    o && n.deps.push(o);
                }
                n.execute = function (e, n, a) { var o; if (t.metadata.globals) {
                    o = {};
                    for (var i in t.metadata.globals)
                        t.metadata.globals[i] && (o[i] = e(t.metadata.globals[i]));
                } var s = t.metadata.exports; s && (t.source += "\n" + ie + '["' + s + '"] = ' + s + ";"); var l = r.get("@@global-helpers").prepareGlobal(a.id, s, o, !!t.metadata.encapsulateGlobal); return ee.call(r, t), l(); };
            } return e.call(this, t); }; }), i("reduceRegister_", function (e) { return function (t, r) { if (r || !t.metadata.exports && (!A || "global" != t.metadata.format))
                return e.call(this, t, r); t.metadata.format = "global"; var n = t.metadata.entry = M(); n.deps = t.metadata.deps; var a = R(t.metadata.exports); n.execute = function () { return a; }; }; }), s(function (t) { return function () { function r(t) { if (Object.keys)
                Object.keys(e).forEach(t);
            else
                for (var r in e)
                    i.call(e, r) && t(r); } function n(t) { r(function (r) { if (U.call(s, r) == -1) {
                try {
                    var n = e[r];
                }
                catch (e) {
                    s.push(r);
                }
                t(r, n);
            } }); } var a = this; t.call(a); var o, i = Object.prototype.hasOwnProperty, s = ["_g", "sessionStorage", "localStorage", "clipboardData", "frames", "frameElement", "external", "mozAnimationStartTime", "webkitStorageInfo", "webkitIndexedDB", "mozInnerScreenY", "mozInnerScreenX"]; a.set("@@global-helpers", a.newModule({ prepareGlobal: function (t, r, a, i) { var s = e.define; e.define = void 0; var l; if (a) {
                    l = {};
                    for (var u in a)
                        l[u] = e[u], e[u] = a[u];
                } return r || (o = {}, n(function (e, t) { o[e] = t; })), function () { var t, a = r ? R(r) : {}, u = !!r; if (r && !i || n(function (n, s) { o[n] !== s && "undefined" != typeof s && (i && (e[n] = void 0), r || (a[n] = s, "undefined" != typeof t ? u || t === s || (u = !0) : t = s)); }), a = u ? a : t, l)
                    for (var d in l)
                        e[d] = l[d]; return e.define = s, a; }; } })); }; }), function () { function t(e) { function t(e, t) { for (var r = 0; r < e.length; r++)
                if (e[r][0] < t.index && e[r][1] > t.index)
                    return !0; return !1; } n.lastIndex = a.lastIndex = o.lastIndex = 0; var r, i = [], s = [], l = []; if (e.length / e.split("\n").length < 200) {
                for (; r = o.exec(e);)
                    s.push([r.index, r.index + r[0].length]);
                for (; r = a.exec(e);)
                    t(s, r) || l.push([r.index + r[1].length, r.index + r[0].length - 1]);
            } for (; r = n.exec(e);)
                if (!t(s, r) && !t(l, r)) {
                    var u = r[1].substr(1, r[1].length - 2);
                    if (u.match(/"|'/))
                        continue;
                    "/" == u[u.length - 1] && (u = u.substr(0, u.length - 1)), i.push(u);
                } return i; } var r = /(?:^\uFEFF?|[^$_a-zA-Z\xA0-\uFFFF.])(exports\s*(\[['"]|\.)|module(\.exports|\['exports'\]|\["exports"\])\s*(\[['"]|[=,\.]))/, n = /(?:^\uFEFF?|[^$_a-zA-Z\xA0-\uFFFF."'])require\s*\(\s*("[^"\\]*(?:\\.[^"\\]*)*"|'[^'\\]*(?:\\.[^'\\]*)*')\s*\)/g, a = /(^|[^\\])(\/\*([\s\S]*?)\*\/|([^:]|^)\/\/(.*)$)/gm, o = /("[^"\\\n\r]*(\\.[^"\\\n\r]*)*"|'[^'\\\n\r]*(\\.[^'\\\n\r]*)*')/g, s = /^\#\!.*/; i("instantiate", function (a) { return function (o) { var i = this; if (o.metadata.format || (r.lastIndex = 0, n.lastIndex = 0, (n.exec(o.source) || r.exec(o.source)) && (o.metadata.format = "cjs")), "cjs" == o.metadata.format) {
                var l = o.metadata.deps, u = o.metadata.cjsRequireDetection === !1 ? [] : t(o.source);
                for (var d in o.metadata.globals)
                    o.metadata.globals[d] && u.push(o.metadata.globals[d]);
                var c = M();
                o.metadata.entry = c, c.deps = u, c.executingRequire = !0, c.execute = function (t, r, n) { function a(e) { return "/" == e[e.length - 1] && (e = e.substr(0, e.length - 1)), t.apply(this, arguments); } if (a.resolve = function (e) { return i.get("@@cjs-helpers").requireResolve(e, n.id); }, n.paths = [], n.require = t, !o.metadata.cjsDeferDepsExecute)
                    for (var u = 0; u < l.length; u++)
                        a(l[u]); var d = i.get("@@cjs-helpers").getPathVars(n.id), c = { exports: r, args: [a, r, n, d.filename, d.dirname, e, e] }, f = "(function(require, exports, module, __filename, __dirname, global, GLOBAL"; if (o.metadata.globals)
                    for (var m in o.metadata.globals)
                        c.args.push(a(o.metadata.globals[m])), f += ", " + m; var p = e.define; e.define = void 0, e.__cjsWrapper = c, o.source = f + ") {" + o.source.replace(s, "") + "\n}).apply(__cjsWrapper.exports, __cjsWrapper.args);", ee.call(i, o), e.__cjsWrapper = void 0, e.define = p; };
            } return a.call(i, o); }; }); }(), s(function (e) { return function () { function t(e) { return "file:///" == e.substr(0, 8) ? e.substr(7 + !!D) : n && e.substr(0, n.length) == n ? e.substr(n.length) : e; } var r = this; if (e.call(r), "undefined" != typeof window && "undefined" != typeof document && window.location)
                var n = location.protocol + "//" + location.hostname + (location.port ? ":" + location.port : ""); r.set("@@cjs-helpers", r.newModule({ requireResolve: function (e, n) { return t(r.normalizeSync(e, n)); }, getPathVars: function (e) { var r, n = e.lastIndexOf("!"); r = n != -1 ? e.substr(0, n) : e; var a = r.split("/"); return a.pop(), a = a.join("/"), { filename: t(r), dirname: t(a) }; } })); }; }), i("fetch", function (t) { return function (r) { return r.metadata.scriptLoad && F && (e.define = this.amdDefine), t.call(this, r); }; }), s(function (t) { return function () { function r(e, t) { e = e.replace(s, ""); var r = e.match(d), n = (r[1].split(",")[t] || "require").replace(c, ""), a = f[n] || (f[n] = new RegExp(l + n + u, "g")); a.lastIndex = 0; for (var o, i = []; o = a.exec(e);)
                i.push(o[2] || o[3]); return i; } function n(e, t, r, a) { if ("object" == typeof e && !(e instanceof Array))
                return n.apply(null, Array.prototype.splice.call(arguments, 1, arguments.length - 1)); if ("string" == typeof e && "function" == typeof t && (e = [e]), !(e instanceof Array)) {
                if ("string" == typeof e) {
                    var i = o.defaultJSExtensions && ".js" != e.substr(e.length - 3, 3), s = o.decanonicalize(e, a);
                    i && ".js" == s.substr(s.length - 3, 3) && (s = s.substr(0, s.length - 3));
                    var l = o.get(s);
                    if (!l)
                        throw new Error('Module not already loaded loading "' + e + '" as ' + s + (a ? ' from "' + a + '".' : "."));
                    return l.__useDefault ? l.default : l;
                }
                throw new TypeError("Invalid require");
            } for (var u = [], d = 0; d < e.length; d++)
                u.push(o.import(e[d], a)); Promise.all(u).then(function (e) { t && t.apply(null, e); }, r); } function a(t, a, i) { function s(t, r, s) { function c(e, r, a) { return "string" == typeof e && "function" != typeof r ? t(e) : n.call(o, e, r, a, s.id); } for (var f = [], m = 0; m < a.length; m++)
                f.push(t(a[m])); s.uri = s.id, s.config = function () { }, d != -1 && f.splice(d, 0, s), u != -1 && f.splice(u, 0, r), l != -1 && (c.toUrl = function (e) { var t = o.defaultJSExtensions && ".js" != e.substr(e.length - 3, 3), r = o.decanonicalize(e, s.id); return t && ".js" == r.substr(r.length - 3, 3) && (r = r.substr(0, r.length - 3)), r; }, f.splice(l, 0, c)); var p = e.require; e.require = n; var h = i.apply(u == -1 ? e : r, f); if (e.require = p, "undefined" == typeof h && s && (h = s.exports), "undefined" != typeof h)
                return h; } "string" != typeof t && (i = a, a = t, t = null), a instanceof Array || (i = a, a = ["require", "exports", "module"].splice(0, i.length)), "function" != typeof i && (i = function (e) { return function () { return e; }; }(i)), void 0 === a[a.length - 1] && a.pop(); var l, u, d; (l = U.call(a, "require")) != -1 && (a.splice(l, 1), t || (a = a.concat(r(i.toString(), l)))), (u = U.call(a, "exports")) != -1 && a.splice(u, 1), (d = U.call(a, "module")) != -1 && a.splice(d, 1); var c = M(); c.name = t && (o.decanonicalize || o.normalize).call(o, t), c.deps = a, c.execute = s, o.pushRegister_({ amd: !0, entry: c }); } var o = this; t.call(this); var s = /(\/\*([\s\S]*?)\*\/|([^:]|^)\/\/(.*)$)/gm, l = "(?:^|[^$_a-zA-Z\\xA0-\\uFFFF.])", u = "\\s*\\(\\s*(\"([^\"]+)\"|'([^']+)')\\s*\\)", d = /\(([^\)]*)\)/, c = /^\s+|\s+$/g, f = {}; a.amd = {}, i("reduceRegister_", function (e) { return function (t, r) { if (!r || !r.amd)
                return e.call(this, t, r); var n = t && t.metadata, a = r.entry; if (n)
                if (n.format && "detect" != n.format) {
                    if (!a.name && "amd" != n.format)
                        throw new Error("AMD define called while executing " + n.format + " module " + t.name);
                }
                else
                    n.format = "amd"; if (a.name)
                n && (n.entry || n.bundle ? n.entry && n.entry.name && n.entry.name != t.name && (n.entry = void 0) : n.entry = a, n.bundle = !0), a.name in this.defined || (this.defined[a.name] = a);
            else {
                if (!n)
                    throw new TypeError("Unexpected anonymous AMD define.");
                if (n.entry && !n.entry.name)
                    throw new Error("Multiple anonymous defines in module " + t.name);
                n.entry = a;
            } }; }), o.amdDefine = a, o.amdRequire = n; }; }), function () { var t = /(?:^\uFEFF?|[^$_a-zA-Z\xA0-\uFFFF.])define\s*\(\s*("[^"]+"\s*,\s*|'[^']+'\s*,\s*)?\s*(\[(\s*(("[^"]+"|'[^']+')\s*,|\/\/.*\r?\n|\/\*(.|\s)*?\*\/))*(\s*("[^"]+"|'[^']+')\s*,?)?(\s*(\/\/.*\r?\n|\/\*(.|\s)*?\*\/))*\s*\]|function\s*|{|[_$a-zA-Z\xA0-\uFFFF][_$a-zA-Z0-9\xA0-\uFFFF]*\))/; i("instantiate", function (r) { return function (n) { var a = this; if ("amd" == n.metadata.format || !n.metadata.format && n.source.match(t))
                if (n.metadata.format = "amd", a.builder || a.execute === !1)
                    n.metadata.execute = function () { return n.metadata.builderExecute.apply(this, arguments); };
                else {
                    var o = e.define;
                    e.define = this.amdDefine;
                    try {
                        ee.call(a, n);
                    }
                    finally {
                        e.define = o;
                    }
                    if (!n.metadata.entry && !n.metadata.bundle)
                        throw new TypeError("AMD module " + n.name + " did not define");
                } return r.call(a, n); }; }); }(), function () { function e(e, t) { if (t) {
                var r;
                if (e.pluginFirst) {
                    if ((r = t.lastIndexOf("!")) != -1)
                        return t.substr(r + 1);
                }
                else if ((r = t.indexOf("!")) != -1)
                    return t.substr(0, r);
                return t;
            } } function t(e, t) { var r, n, a = t.lastIndexOf("!"); if (a != -1)
                return e.pluginFirst ? (r = t.substr(a + 1), n = t.substr(0, a)) : (r = t.substr(0, a), n = t.substr(a + 1) || r.substr(r.lastIndexOf(".") + 1)), { argument: r, plugin: n }; } function r(e, t, r, n) { return n && ".js" == t.substr(t.length - 3, 3) && (t = t.substr(0, t.length - 3)), e.pluginFirst ? r + "!" + t : t + "!" + r; } function n(e, t) { return e.defaultJSExtensions && ".js" != t.substr(t.length - 3, 3); } function a(a) { return function (o, i, s) { var l = this, u = t(l, o); if (i = e(this, i), !u)
                return a.call(this, o, i, s); var d = l.normalizeSync(u.argument, i, !0), c = l.normalizeSync(u.plugin, i, !0); return r(l, d, c, n(l, u.argument)); }; } i("decanonicalize", a), i("normalizeSync", a), i("normalize", function (a) { return function (o, i, s) { var l = this; i = e(this, i); var u = t(l, o); return u ? Promise.all([l.normalize(u.argument, i, !0), l.normalize(u.plugin, i, !1)]).then(function (e) { return r(l, e[0], e[1], n(l, u.argument)); }) : a.call(l, o, i, s); }; }), i("locate", function (e) { return function (t) { var r, n = this, a = t.name; return n.pluginFirst ? (r = a.indexOf("!")) != -1 && (t.metadata.loader = a.substr(0, r), t.name = a.substr(r + 1)) : (r = a.lastIndexOf("!")) != -1 && (t.metadata.loader = a.substr(r + 1), t.name = a.substr(0, r)), e.call(n, t).then(function (e) { return r == -1 && t.metadata.loader ? (n.pluginLoader || n).normalize(t.metadata.loader, t.name).then(function (r) { return t.metadata.loader = r, e; }) : e; }).then(function (e) { var r = t.metadata.loader; if (!r)
                return e; if (t.name == r)
                throw new Error("Plugin " + r + " cannot load itself, make sure it is excluded from any wildcard meta configuration via a custom loader: false rule."); if (n.defined && n.defined[a])
                return e; var o = n.pluginLoader || n; return o.import(r).then(function (r) { return t.metadata.loaderModule = r, t.address = e, r.locate ? r.locate.call(n, t) : e; }); }); }; }), i("fetch", function (e) { return function (t) { var r = this; return t.metadata.loaderModule && t.metadata.loaderModule.fetch && "defined" != t.metadata.format ? (t.metadata.scriptLoad = !1, t.metadata.loaderModule.fetch.call(r, t, function (t) { return e.call(r, t); })) : e.call(r, t); }; }), i("translate", function (e) { return function (t) { var r = this, n = arguments; return t.metadata.loaderModule && t.metadata.loaderModule.translate && "defined" != t.metadata.format ? Promise.resolve(t.metadata.loaderModule.translate.apply(r, n)).then(function (a) { var o = t.metadata.sourceMap; if (o) {
                if ("object" != typeof o)
                    throw new Error("load.metadata.sourceMap must be set to an object.");
                var i = t.address.split("!")[0];
                o.file && o.file != t.address || (o.file = i + "!transpiled"), (!o.sources || o.sources.length <= 1 && (!o.sources[0] || o.sources[0] == t.address)) && (o.sources = [i]);
            } return "string" == typeof a ? t.source = a : w.call(this, "Plugin " + t.metadata.loader + " should return the source in translate, instead of setting load.source directly. This support will be deprecated."), e.apply(r, n); }) : e.apply(r, n); }; }), i("instantiate", function (e) { return function (t) { var r = this, n = !1; return t.metadata.loaderModule && t.metadata.loaderModule.instantiate && !r.builder && "defined" != t.metadata.format ? Promise.resolve(t.metadata.loaderModule.instantiate.call(r, t, function (t) { if (n)
                throw new Error("Instantiate must only be called once."); return n = !0, e.call(r, t); })).then(function (a) { return n ? a : (t.metadata.entry = M(), t.metadata.entry.execute = function () { return a; }, t.metadata.entry.deps = t.metadata.deps, t.metadata.format = "defined", e.call(r, t)); }) : e.call(r, t); }; }); }();
            var se = ["browser", "node", "dev", "build", "production", "default"], le = /#\{[^\}]+\}/;
            i("normalize", function (e) { return function (t, r, n) { var a = this; return C.call(a, t, r).then(function (t) { return e.call(a, t, r, n); }).then(function (e) { return L.call(a, e, r); }); }; }), function () { i("fetch", function (e) { return function (t) { var r = t.metadata.alias, n = t.metadata.deps || []; if (r) {
                t.metadata.format = "defined";
                var a = M();
                return this.defined[t.name] = a, a.declarative = !0, a.deps = n.concat([r]), a.declare = function (e) { return { setters: [function (t) { for (var r in t)
                            e(r, t[r]); t.__useDefault && (a.module.exports.__useDefault = !0); }], execute: function () { } }; }, "";
            } return e.call(this, t); }; }); }(), function () { function e(e, t, r) { for (var n, a = t.split("."); a.length > 1;)
                n = a.shift(), e = e[n] = e[n] || {}; n = a.shift(), n in e || (e[n] = r); } s(function (e) { return function () { this.meta = {}, e.call(this); }; }), i("locate", function (e) { return function (t) { var r, n = this.meta, a = t.name, o = 0; for (var i in n)
                if (r = i.indexOf("*"), r !== -1 && i.substr(0, r) === a.substr(0, r) && i.substr(r + 1) === a.substr(a.length - i.length + r + 1)) {
                    var s = i.split("/").length;
                    s > o && (o = s), v(t.metadata, n[i], o != s);
                } return n[a] && v(t.metadata, n[a]), e.call(this, t); }; }); var t = /^(\s*\/\*[^\*]*(\*(?!\/)[^\*]*)*\*\/|\s*\/\/[^\n]*|\s*"[^"]+"\s*;?|\s*'[^']+'\s*;?)+/, r = /\/\*[^\*]*(\*(?!\/)[^\*]*)*\*\/|\/\/[^\n]*|"[^"]+"\s*;?|'[^']+'\s*;?/g; i("translate", function (n) { return function (a) { if ("defined" == a.metadata.format)
                return a.metadata.deps = a.metadata.deps || [], Promise.resolve(a.source); var o = a.source.match(t); if (o)
                for (var i = o[0].match(r), s = 0; s < i.length; s++) {
                    var l = i[s], u = l.length, d = l.substr(0, 1);
                    if (";" == l.substr(u - 1, 1) && u--, '"' == d || "'" == d) {
                        var c = l.substr(1, l.length - 3), f = c.substr(0, c.indexOf(" "));
                        if (f) {
                            var m = c.substr(f.length + 1, c.length - f.length - 1);
                            "[]" == f.substr(f.length - 2, 2) ? (f = f.substr(0, f.length - 2), a.metadata[f] = a.metadata[f] || [], a.metadata[f].push(m)) : a.metadata[f] instanceof Array ? (w.call(this, "Module " + a.name + ' contains deprecated "deps ' + m + '" meta syntax.\nThis should be updated to "deps[] ' + m + '" for pushing to array meta.'), a.metadata[f].push(m)) : e(a.metadata, f, m);
                        }
                        else
                            a.metadata[c] = !0;
                    }
                } return n.apply(this, arguments); }; }); }(), function () { s(function (e) { return function () { e.call(this), this.bundles = {}, this._loader.loadedBundles = {}; }; }), i("locate", function (e) { return function (t) { var r = this, n = !1; if (!(t.name in r.defined))
                for (var a in r.bundles) {
                    for (var o = 0; o < r.bundles[a].length; o++) {
                        var i = r.bundles[a][o];
                        if (i == t.name) {
                            n = !0;
                            break;
                        }
                        if (i.indexOf("*") != -1) {
                            var s = i.split("*");
                            if (2 != s.length) {
                                r.bundles[a].splice(o--, 1);
                                continue;
                            }
                            if (t.name.substring(0, s[0].length) == s[0] && t.name.substr(t.name.length - s[1].length, s[1].length) == s[1] && t.name.substr(s[0].length, t.name.length - s[1].length - s[0].length).indexOf("/") == -1) {
                                n = !0;
                                break;
                            }
                        }
                    }
                    if (n)
                        return r.import(a).then(function () { return e.call(r, t); });
                } return e.call(r, t); }; }); }(), function () { s(function (e) { return function () { e.call(this), this.depCache = {}; }; }), i("locate", function (e) { return function (t) { var r = this, n = r.depCache[t.name]; if (n)
                for (var a = 0; a < n.length; a++)
                    r.import(n[a], t.name); return e.call(r, t); }; }); }(), X = new a, e.SystemJS = X, X.version = "0.19.40 Standard", "object" == typeof module && module.exports && "object" == typeof exports && (module.exports = X), e.System = X;
        }("undefined" != typeof self ? self : global);
    }
    var t = "undefined" == typeof Promise;
    if ("undefined" != typeof document) {
        var r = document.getElementsByTagName("script");
        if ($__curScript = r[r.length - 1], document.currentScript && ($__curScript.defer || $__curScript.async) && ($__curScript = document.currentScript), $__curScript.src || ($__curScript = void 0), t) {
            var n = $__curScript.src, a = n.substr(0, n.lastIndexOf("/") + 1);
            window.systemJSBootstrap = e, document.write('<script type="text/javascript" src="' + a + 'system-polyfills.js"></script>');
        }
        else
            e();
    }
    else if ("undefined" != typeof importScripts) {
        var a = "";
        try {
            throw new Error("_");
        }
        catch (e) {
            e.stack.replace(/(?:at|@).*(http.+):[\d]+:[\d]+/, function (e, t) { $__curScript = { src: t }, a = t.replace(/\/[^\/]*$/, "/"); });
        }
        t && importScripts(a + "system-polyfills.js"), e();
    }
    else
        $__curScript = "undefined" != typeof __filename ? { src: __filename } : null, e();
}();
//# sourceMappingURL=system.js.map
var fabric = fabric || { version: "1.6.6" };
"undefined" != typeof exports && (exports.fabric = fabric), "undefined" != typeof document && "undefined" != typeof window ? (fabric.document = document, fabric.window = window, window.fabric = fabric) : (fabric.document = require("jsdom").jsdom("<!DOCTYPE html><html><head></head><body></body></html>"), fabric.document.createWindow ? fabric.window = fabric.document.createWindow() : fabric.window = fabric.document.parentWindow), fabric.isTouchSupported = "ontouchstart" in fabric.document.documentElement, fabric.isLikelyNode = "undefined" != typeof Buffer && "undefined" == typeof window, fabric.SHARED_ATTRIBUTES = ["display", "transform", "fill", "fill-opacity", "fill-rule", "opacity", "stroke", "stroke-dasharray", "stroke-linecap", "stroke-linejoin", "stroke-miterlimit", "stroke-opacity", "stroke-width", "id"], fabric.DPI = 96, fabric.reNum = "(?:[-+]?(?:\\d+|\\d*\\.\\d+)(?:e[-+]?\\d+)?)", fabric.fontPaths = {}, fabric.charWidthsCache = {}, fabric.devicePixelRatio = fabric.window.devicePixelRatio || fabric.window.webkitDevicePixelRatio || fabric.window.mozDevicePixelRatio || 1, function () { function t(t, e) { if (this.__eventListeners[t]) {
    var i = this.__eventListeners[t];
    e ? i[i.indexOf(e)] = !1 : fabric.util.array.fill(i, !1);
} } function e(t, e) { if (this.__eventListeners || (this.__eventListeners = {}), 1 === arguments.length)
    for (var i in t)
        this.on(i, t[i]);
else
    this.__eventListeners[t] || (this.__eventListeners[t] = []), this.__eventListeners[t].push(e); return this; } function i(e, i) { if (this.__eventListeners) {
    if (0 === arguments.length)
        for (e in this.__eventListeners)
            t.call(this, e);
    else if (1 === arguments.length && "object" == typeof arguments[0])
        for (var r in e)
            t.call(this, r, e[r]);
    else
        t.call(this, e, i);
    return this;
} } function r(t, e) { if (this.__eventListeners) {
    var i = this.__eventListeners[t];
    if (i) {
        for (var r = 0, n = i.length; r < n; r++)
            i[r] && i[r].call(this, e || {});
        return this.__eventListeners[t] = i.filter(function (t) { return t !== !1; }), this;
    }
} } fabric.Observable = { observe: e, stopObserving: i, fire: r, on: e, off: i, trigger: r }; }(), fabric.Collection = { _objects: [], add: function () { if (this._objects.push.apply(this._objects, arguments), this._onObjectAdded)
        for (var t = 0, e = arguments.length; t < e; t++)
            this._onObjectAdded(arguments[t]); return this.renderOnAddRemove && this.renderAll(), this; }, insertAt: function (t, e, i) { var r = this.getObjects(); return i ? r[e] = t : r.splice(e, 0, t), this._onObjectAdded && this._onObjectAdded(t), this.renderOnAddRemove && this.renderAll(), this; }, remove: function () { for (var t, e = this.getObjects(), i = !1, r = 0, n = arguments.length; r < n; r++)
        t = e.indexOf(arguments[r]), t !== -1 && (i = !0, e.splice(t, 1), this._onObjectRemoved && this._onObjectRemoved(arguments[r])); return this.renderOnAddRemove && i && this.renderAll(), this; }, forEachObject: function (t, e) { for (var i = this.getObjects(), r = 0, n = i.length; r < n; r++)
        t.call(e, i[r], r, i); return this; }, getObjects: function (t) { return "undefined" == typeof t ? this._objects : this._objects.filter(function (e) { return e.type === t; }); }, item: function (t) { return this.getObjects()[t]; }, isEmpty: function () { return 0 === this.getObjects().length; }, size: function () { return this.getObjects().length; }, contains: function (t) { return this.getObjects().indexOf(t) > -1; }, complexity: function () { return this.getObjects().reduce(function (t, e) { return t += e.complexity ? e.complexity() : 0; }, 0); } }, function (t) { var e = Math.sqrt, i = Math.atan2, r = Math.pow, n = Math.abs, s = Math.PI / 180; fabric.util = { removeFromArray: function (t, e) { var i = t.indexOf(e); return i !== -1 && t.splice(i, 1), t; }, getRandomInt: function (t, e) { return Math.floor(Math.random() * (e - t + 1)) + t; }, degreesToRadians: function (t) { return t * s; }, radiansToDegrees: function (t) { return t / s; }, rotatePoint: function (t, e, i) { t.subtractEquals(e); var r = fabric.util.rotateVector(t, i); return new fabric.Point(r.x, r.y).addEquals(e); }, rotateVector: function (t, e) { var i = Math.sin(e), r = Math.cos(e), n = t.x * r - t.y * i, s = t.x * i + t.y * r; return { x: n, y: s }; }, transformPoint: function (t, e, i) { return i ? new fabric.Point(e[0] * t.x + e[2] * t.y, e[1] * t.x + e[3] * t.y) : new fabric.Point(e[0] * t.x + e[2] * t.y + e[4], e[1] * t.x + e[3] * t.y + e[5]); }, makeBoundingBoxFromPoints: function (t) { var e = [t[0].x, t[1].x, t[2].x, t[3].x], i = fabric.util.array.min(e), r = fabric.util.array.max(e), n = Math.abs(i - r), s = [t[0].y, t[1].y, t[2].y, t[3].y], o = fabric.util.array.min(s), a = fabric.util.array.max(s), h = Math.abs(o - a); return { left: i, top: o, width: n, height: h }; }, invertTransform: function (t) { var e = 1 / (t[0] * t[3] - t[1] * t[2]), i = [e * t[3], -e * t[1], -e * t[2], e * t[0]], r = fabric.util.transformPoint({ x: t[4], y: t[5] }, i, !0); return i[4] = -r.x, i[5] = -r.y, i; }, toFixed: function (t, e) { return parseFloat(Number(t).toFixed(e)); }, parseUnit: function (t, e) { var i = /\D{0,2}$/.exec(t), r = parseFloat(t); switch (e || (e = fabric.Text.DEFAULT_SVG_FONT_SIZE), i[0]) {
        case "mm": return r * fabric.DPI / 25.4;
        case "cm": return r * fabric.DPI / 2.54;
        case "in": return r * fabric.DPI;
        case "pt": return r * fabric.DPI / 72;
        case "pc": return r * fabric.DPI / 72 * 12;
        case "em": return r * e;
        default: return r;
    } }, falseFunction: function () { return !1; }, getKlass: function (t, e) { return t = fabric.util.string.camelize(t.charAt(0).toUpperCase() + t.slice(1)), fabric.util.resolveNamespace(e)[t]; }, resolveNamespace: function (e) { if (!e)
        return fabric; var i, r = e.split("."), n = r.length, s = t || fabric.window; for (i = 0; i < n; ++i)
        s = s[r[i]]; return s; }, loadImage: function (t, e, i, r) { if (!t)
        return void (e && e.call(i, t)); var n = fabric.util.createImage(); n.onload = function () { e && e.call(i, n), n = n.onload = n.onerror = null; }, n.onerror = function () { fabric.log("Error loading " + n.src), e && e.call(i, null, !0), n = n.onload = n.onerror = null; }, 0 !== t.indexOf("data") && r && (n.crossOrigin = r), n.src = t; }, enlivenObjects: function (t, e, i, r) { function n() { ++o === a && e && e(s); } t = t || []; var s = [], o = 0, a = t.length; return a ? void t.forEach(function (t, e) { if (!t || !t.type)
        return void n(); var o = fabric.util.getKlass(t.type, i); o.async ? o.fromObject(t, function (i, o) { o || (s[e] = i, r && r(t, s[e])), n(); }) : (s[e] = o.fromObject(t), r && r(t, s[e]), n()); }) : void (e && e(s)); }, groupSVGElements: function (t, e, i) { var r; return r = new fabric.PathGroup(t, e), "undefined" != typeof i && r.setSourcePath(i), r; }, populateWithProperties: function (t, e, i) { if (i && "[object Array]" === Object.prototype.toString.call(i))
        for (var r = 0, n = i.length; r < n; r++)
            i[r] in t && (e[i[r]] = t[i[r]]); }, drawDashedLine: function (t, r, n, s, o, a) { var h = s - r, c = o - n, l = e(h * h + c * c), u = i(c, h), f = a.length, d = 0, g = !0; for (t.save(), t.translate(r, n), t.moveTo(0, 0), t.rotate(u), r = 0; l > r;)
        r += a[d++ % f], r > l && (r = l), t[g ? "lineTo" : "moveTo"](r, 0), g = !g; t.restore(); }, createCanvasElement: function (t) { return t || (t = fabric.document.createElement("canvas")), t.getContext || "undefined" == typeof G_vmlCanvasManager || G_vmlCanvasManager.initElement(t), t; }, createImage: function () { return fabric.isLikelyNode ? new (require("canvas").Image) : fabric.document.createElement("img"); }, createAccessors: function (t) { var e, i, r, n, s, o = t.prototype; for (e = o.stateProperties.length; e--;)
        i = o.stateProperties[e], r = i.charAt(0).toUpperCase() + i.slice(1), n = "set" + r, s = "get" + r, o[s] || (o[s] = function (t) { return new Function('return this.get("' + t + '")'); }(i)), o[n] || (o[n] = function (t) { return new Function("value", 'return this.set("' + t + '", value)'); }(i)); }, clipContext: function (t, e) { e.save(), e.beginPath(), t.clipTo(e), e.clip(); }, multiplyTransformMatrices: function (t, e, i) { return [t[0] * e[0] + t[2] * e[1], t[1] * e[0] + t[3] * e[1], t[0] * e[2] + t[2] * e[3], t[1] * e[2] + t[3] * e[3], i ? 0 : t[0] * e[4] + t[2] * e[5] + t[4], i ? 0 : t[1] * e[4] + t[3] * e[5] + t[5]]; }, qrDecompose: function (t) { var n = i(t[1], t[0]), o = r(t[0], 2) + r(t[1], 2), a = e(o), h = (t[0] * t[3] - t[2] * t[1]) / a, c = i(t[0] * t[2] + t[1] * t[3], o); return { angle: n / s, scaleX: a, scaleY: h, skewX: c / s, skewY: 0, translateX: t[4], translateY: t[5] }; }, customTransformMatrix: function (t, e, i) { var r = [1, 0, n(Math.tan(i * s)), 1], o = [n(t), 0, 0, n(e)]; return fabric.util.multiplyTransformMatrices(o, r, !0); }, resetObjectTransform: function (t) { t.scaleX = 1, t.scaleY = 1, t.skewX = 0, t.skewY = 0, t.flipX = !1, t.flipY = !1, t.setAngle(0); }, getFunctionBody: function (t) { return (String(t).match(/function[^{]*\{([\s\S]*)\}/) || {})[1]; }, isTransparent: function (t, e, i, r) { r > 0 && (e > r ? e -= r : e = 0, i > r ? i -= r : i = 0); var n, s, o = !0, a = t.getImageData(e, i, 2 * r || 1, 2 * r || 1), h = a.data.length; for (n = 3; n < h && (s = a.data[n], o = s <= 0, o !== !1); n += 4)
        ; return a = null, o; }, parsePreserveAspectRatioAttribute: function (t) { var e, i = "meet", r = "Mid", n = "Mid", s = t.split(" "); return s && s.length && (i = s.pop(), "meet" !== i && "slice" !== i ? (e = i, i = "meet") : s.length && (e = s.pop())), r = "none" !== e ? e.slice(1, 4) : "none", n = "none" !== e ? e.slice(5, 8) : "none", { meetOrSlice: i, alignX: r, alignY: n }; }, clearFabricFontCache: function (t) { t ? fabric.charWidthsCache[t] && delete fabric.charWidthsCache[t] : fabric.charWidthsCache = {}; } }; }("undefined" != typeof exports ? exports : this), function () { function t(t, r, s, o, h, c, l) { var u = a.call(arguments); if (n[u])
    return n[u]; var f = Math.PI, d = l * f / 180, g = Math.sin(d), p = Math.cos(d), v = 0, b = 0; s = Math.abs(s), o = Math.abs(o); var m = -p * t * .5 - g * r * .5, y = -p * r * .5 + g * t * .5, _ = s * s, x = o * o, S = y * y, C = m * m, w = _ * x - _ * S - x * C, O = 0; if (w < 0) {
    var T = Math.sqrt(1 - w / (_ * x));
    s *= T, o *= T;
}
else
    O = (h === c ? -1 : 1) * Math.sqrt(w / (_ * S + x * C)); var k = O * s * y / o, j = -O * o * m / s, M = p * k - g * j + .5 * t, A = g * k + p * j + .5 * r, P = i(1, 0, (m - k) / s, (y - j) / o), I = i((m - k) / s, (y - j) / o, (-m - k) / s, (-y - j) / o); 0 === c && I > 0 ? I -= 2 * f : 1 === c && I < 0 && (I += 2 * f); for (var D = Math.ceil(Math.abs(I / f * 2)), E = [], L = I / D, R = 8 / 3 * Math.sin(L / 4) * Math.sin(L / 4) / Math.sin(L / 2), F = P + L, B = 0; B < D; B++)
    E[B] = e(P, F, p, g, s, o, M, A, R, v, b), v = E[B][4], b = E[B][5], P = F, F += L; return n[u] = E, E; } function e(t, e, i, r, n, o, h, c, l, u, f) { var d = a.call(arguments); if (s[d])
    return s[d]; var g = Math.cos(t), p = Math.sin(t), v = Math.cos(e), b = Math.sin(e), m = i * n * v - r * o * b + h, y = r * n * v + i * o * b + c, _ = u + l * (-i * n * p - r * o * g), x = f + l * (-r * n * p + i * o * g), S = m + l * (i * n * b + r * o * v), C = y + l * (r * n * b - i * o * v); return s[d] = [_, x, S, C, m, y], s[d]; } function i(t, e, i, r) { var n = Math.atan2(e, t), s = Math.atan2(r, i); return s >= n ? s - n : 2 * Math.PI - (n - s); } function r(t, e, i, r, n, s, h, c) { var l = a.call(arguments); if (o[l])
    return o[l]; var u, f, d, g, p, v, b, m, y = Math.sqrt, _ = Math.min, x = Math.max, S = Math.abs, C = [], w = [[], []]; f = 6 * t - 12 * i + 6 * n, u = -3 * t + 9 * i - 9 * n + 3 * h, d = 3 * i - 3 * t; for (var O = 0; O < 2; ++O)
    if (O > 0 && (f = 6 * e - 12 * r + 6 * s, u = -3 * e + 9 * r - 9 * s + 3 * c, d = 3 * r - 3 * e), S(u) < 1e-12) {
        if (S(f) < 1e-12)
            continue;
        g = -d / f, 0 < g && g < 1 && C.push(g);
    }
    else
        b = f * f - 4 * d * u, b < 0 || (m = y(b), p = (-f + m) / (2 * u), 0 < p && p < 1 && C.push(p), v = (-f - m) / (2 * u), 0 < v && v < 1 && C.push(v)); for (var T, k, j, M = C.length, A = M; M--;)
    g = C[M], j = 1 - g, T = j * j * j * t + 3 * j * j * g * i + 3 * j * g * g * n + g * g * g * h, w[0][M] = T, k = j * j * j * e + 3 * j * j * g * r + 3 * j * g * g * s + g * g * g * c, w[1][M] = k; w[0][A] = t, w[1][A] = e, w[0][A + 1] = h, w[1][A + 1] = c; var P = [{ x: _.apply(null, w[0]), y: _.apply(null, w[1]) }, { x: x.apply(null, w[0]), y: x.apply(null, w[1]) }]; return o[l] = P, P; } var n = {}, s = {}, o = {}, a = Array.prototype.join; fabric.util.drawArc = function (e, i, r, n) { for (var s = n[0], o = n[1], a = n[2], h = n[3], c = n[4], l = n[5], u = n[6], f = [[], [], [], []], d = t(l - i, u - r, s, o, h, c, a), g = 0, p = d.length; g < p; g++)
    f[g][0] = d[g][0] + i, f[g][1] = d[g][1] + r, f[g][2] = d[g][2] + i, f[g][3] = d[g][3] + r, f[g][4] = d[g][4] + i, f[g][5] = d[g][5] + r, e.bezierCurveTo.apply(e, f[g]); }, fabric.util.getBoundsOfArc = function (e, i, n, s, o, a, h, c, l) { for (var u, f = 0, d = 0, g = [], p = t(c - e, l - i, n, s, a, h, o), v = 0, b = p.length; v < b; v++)
    u = r(f, d, p[v][0], p[v][1], p[v][2], p[v][3], p[v][4], p[v][5]), g.push({ x: u[0].x + e, y: u[0].y + i }), g.push({ x: u[1].x + e, y: u[1].y + i }), f = p[v][4], d = p[v][5]; return g; }, fabric.util.getBoundsOfCurve = r; }(), function () { function t(t, e) { for (var i = s.call(arguments, 2), r = [], n = 0, o = t.length; n < o; n++)
    r[n] = i.length ? t[n][e].apply(t[n], i) : t[n][e].call(t[n]); return r; } function e(t, e) { return n(t, e, function (t, e) { return t >= e; }); } function i(t, e) { return n(t, e, function (t, e) { return t < e; }); } function r(t, e) { for (var i = t.length; i--;)
    t[i] = e; return t; } function n(t, e, i) { if (t && 0 !== t.length) {
    var r = t.length - 1, n = e ? t[r][e] : t[r];
    if (e)
        for (; r--;)
            i(t[r][e], n) && (n = t[r][e]);
    else
        for (; r--;)
            i(t[r], n) && (n = t[r]);
    return n;
} } var s = Array.prototype.slice; Array.prototype.indexOf || (Array.prototype.indexOf = function (t) { if (void 0 === this || null === this)
    throw new TypeError; var e = Object(this), i = e.length >>> 0; if (0 === i)
    return -1; var r = 0; if (arguments.length > 0 && (r = Number(arguments[1]), r !== r ? r = 0 : 0 !== r && r !== Number.POSITIVE_INFINITY && r !== Number.NEGATIVE_INFINITY && (r = (r > 0 || -1) * Math.floor(Math.abs(r)))), r >= i)
    return -1; for (var n = r >= 0 ? r : Math.max(i - Math.abs(r), 0); n < i; n++)
    if (n in e && e[n] === t)
        return n; return -1; }), Array.prototype.forEach || (Array.prototype.forEach = function (t, e) { for (var i = 0, r = this.length >>> 0; i < r; i++)
    i in this && t.call(e, this[i], i, this); }), Array.prototype.map || (Array.prototype.map = function (t, e) { for (var i = [], r = 0, n = this.length >>> 0; r < n; r++)
    r in this && (i[r] = t.call(e, this[r], r, this)); return i; }), Array.prototype.every || (Array.prototype.every = function (t, e) { for (var i = 0, r = this.length >>> 0; i < r; i++)
    if (i in this && !t.call(e, this[i], i, this))
        return !1; return !0; }), Array.prototype.some || (Array.prototype.some = function (t, e) { for (var i = 0, r = this.length >>> 0; i < r; i++)
    if (i in this && t.call(e, this[i], i, this))
        return !0; return !1; }), Array.prototype.filter || (Array.prototype.filter = function (t, e) { for (var i, r = [], n = 0, s = this.length >>> 0; n < s; n++)
    n in this && (i = this[n], t.call(e, i, n, this) && r.push(i)); return r; }), Array.prototype.reduce || (Array.prototype.reduce = function (t) { var e, i = this.length >>> 0, r = 0; if (arguments.length > 1)
    e = arguments[1];
else
    for (;;) {
        if (r in this) {
            e = this[r++];
            break;
        }
        if (++r >= i)
            throw new TypeError;
    } for (; r < i; r++)
    r in this && (e = t.call(null, e, this[r], r, this)); return e; }), fabric.util.array = { fill: r, invoke: t, min: i, max: e }; }(), function () { function t(t, i, r) { if (r)
    if (!fabric.isLikelyNode && i instanceof Element)
        t = i;
    else if (i instanceof Array)
        t = i.map(function (t) { return e(t, r); });
    else if (i instanceof Object)
        for (var n in i)
            t[n] = e(i[n], r);
    else
        t = i;
else
    for (var n in i)
        t[n] = i[n]; return t; } function e(e, i) { return t({}, e, i); } fabric.util.object = { extend: t, clone: e }; }(), function () { function t(t) { return t.replace(/-+(.)?/g, function (t, e) { return e ? e.toUpperCase() : ""; }); } function e(t, e) { return t.charAt(0).toUpperCase() + (e ? t.slice(1) : t.slice(1).toLowerCase()); } function i(t) { return t.replace(/&/g, "&amp;").replace(/"/g, "&quot;").replace(/'/g, "&apos;").replace(/</g, "&lt;").replace(/>/g, "&gt;"); } String.prototype.trim || (String.prototype.trim = function () { return this.replace(/^[\s\xA0]+/, "").replace(/[\s\xA0]+$/, ""); }), fabric.util.string = { camelize: t, capitalize: e, escapeXml: i }; }(), function () { var t = Array.prototype.slice, e = Function.prototype.apply, i = function () { }; Function.prototype.bind || (Function.prototype.bind = function (r) { var n, s = this, o = t.call(arguments, 1); return n = o.length ? function () { return e.call(s, this instanceof i ? this : r, o.concat(t.call(arguments))); } : function () { return e.call(s, this instanceof i ? this : r, arguments); }, i.prototype = this.prototype, n.prototype = new i, n; }); }(), function () { function t() { } function e(t) { var e = this.constructor.superclass.prototype[t]; return arguments.length > 1 ? e.apply(this, r.call(arguments, 1)) : e.call(this); } function i() { function i() { this.initialize.apply(this, arguments); } var s = null, a = r.call(arguments, 0); "function" == typeof a[0] && (s = a.shift()), i.superclass = s, i.subclasses = [], s && (t.prototype = s.prototype, i.prototype = new t, s.subclasses.push(i)); for (var h = 0, c = a.length; h < c; h++)
    o(i, a[h], s); return i.prototype.initialize || (i.prototype.initialize = n), i.prototype.constructor = i, i.prototype.callSuper = e, i; } var r = Array.prototype.slice, n = function () { }, s = function () { for (var t in { toString: 1 })
    if ("toString" === t)
        return !1; return !0; }(), o = function (t, e, i) { for (var r in e)
    r in t.prototype && "function" == typeof t.prototype[r] && (e[r] + "").indexOf("callSuper") > -1 ? t.prototype[r] = function (t) { return function () { var r = this.constructor.superclass; this.constructor.superclass = i; var n = e[t].apply(this, arguments); if (this.constructor.superclass = r, "initialize" !== t)
        return n; }; }(r) : t.prototype[r] = e[r], s && (e.toString !== Object.prototype.toString && (t.prototype.toString = e.toString), e.valueOf !== Object.prototype.valueOf && (t.prototype.valueOf = e.valueOf)); }; fabric.util.createClass = i; }(), function () { function t(t) { var e, i, r = Array.prototype.slice.call(arguments, 1), n = r.length; for (i = 0; i < n; i++)
    if (e = typeof t[r[i]], !/^(?:function|object|unknown)$/.test(e))
        return !1; return !0; } function e(t, e) { return { handler: e, wrappedHandler: i(t, e) }; } function i(t, e) { return function (i) { e.call(o(t), i || fabric.window.event); }; } function r(t, e) { return function (i) { if (p[t] && p[t][e])
    for (var r = p[t][e], n = 0, s = r.length; n < s; n++)
        r[n].call(this, i || fabric.window.event); }; } function n(t) { t || (t = fabric.window.event); var e = t.target || (typeof t.srcElement !== h ? t.srcElement : null), i = fabric.util.getScrollLeftTop(e); return { x: v(t) + i.left, y: b(t) + i.top }; } function s(t, e, i) { var r = "touchend" === t.type ? "changedTouches" : "touches"; return t[r] && t[r][0] ? t[r][0][e] - (t[r][0][e] - t[r][0][i]) || t[i] : t[i]; } var o, a, h = "unknown", c = function () { var t = 0; return function (e) { return e.__uniqueID || (e.__uniqueID = "uniqueID__" + t++); }; }(); !function () { var t = {}; o = function (e) { return t[e]; }, a = function (e, i) { t[e] = i; }; }(); var l, u, f = t(fabric.document.documentElement, "addEventListener", "removeEventListener") && t(fabric.window, "addEventListener", "removeEventListener"), d = t(fabric.document.documentElement, "attachEvent", "detachEvent") && t(fabric.window, "attachEvent", "detachEvent"), g = {}, p = {}; f ? (l = function (t, e, i) { t.addEventListener(e, i, !1); }, u = function (t, e, i) { t.removeEventListener(e, i, !1); }) : d ? (l = function (t, i, r) { var n = c(t); a(n, t), g[n] || (g[n] = {}), g[n][i] || (g[n][i] = []); var s = e(n, r); g[n][i].push(s), t.attachEvent("on" + i, s.wrappedHandler); }, u = function (t, e, i) { var r, n = c(t); if (g[n] && g[n][e])
    for (var s = 0, o = g[n][e].length; s < o; s++)
        r = g[n][e][s], r && r.handler === i && (t.detachEvent("on" + e, r.wrappedHandler), g[n][e][s] = null); }) : (l = function (t, e, i) { var n = c(t); if (p[n] || (p[n] = {}), !p[n][e]) {
    p[n][e] = [];
    var s = t["on" + e];
    s && p[n][e].push(s), t["on" + e] = r(n, e);
} p[n][e].push(i); }, u = function (t, e, i) { var r = c(t); if (p[r] && p[r][e])
    for (var n = p[r][e], s = 0, o = n.length; s < o; s++)
        n[s] === i && n.splice(s, 1); }), fabric.util.addListener = l, fabric.util.removeListener = u; var v = function (t) { return typeof t.clientX !== h ? t.clientX : 0; }, b = function (t) { return typeof t.clientY !== h ? t.clientY : 0; }; fabric.isTouchSupported && (v = function (t) { return s(t, "pageX", "clientX"); }, b = function (t) { return s(t, "pageY", "clientY"); }), fabric.util.getPointer = n, fabric.util.object.extend(fabric.util, fabric.Observable); }(), function () { function t(t, e) { var i = t.style; if (!i)
    return t; if ("string" == typeof e)
    return t.style.cssText += ";" + e, e.indexOf("opacity") > -1 ? s(t, e.match(/opacity:\s*(\d?\.?\d*)/)[1]) : t; for (var r in e)
    if ("opacity" === r)
        s(t, e[r]);
    else {
        var n = "float" === r || "cssFloat" === r ? "undefined" == typeof i.styleFloat ? "cssFloat" : "styleFloat" : r;
        i[n] = e[r];
    } return t; } var e = fabric.document.createElement("div"), i = "string" == typeof e.style.opacity, r = "string" == typeof e.style.filter, n = /alpha\s*\(\s*opacity\s*=\s*([^\)]+)\)/, s = function (t) { return t; }; i ? s = function (t, e) { return t.style.opacity = e, t; } : r && (s = function (t, e) { var i = t.style; return t.currentStyle && !t.currentStyle.hasLayout && (i.zoom = 1), n.test(i.filter) ? (e = e >= .9999 ? "" : "alpha(opacity=" + 100 * e + ")", i.filter = i.filter.replace(n, e)) : i.filter += " alpha(opacity=" + 100 * e + ")", t; }), fabric.util.setStyle = t; }(), function () { function t(t) { return "string" == typeof t ? fabric.document.getElementById(t) : t; } function e(t, e) { var i = fabric.document.createElement(t); for (var r in e)
    "class" === r ? i.className = e[r] : "for" === r ? i.htmlFor = e[r] : i.setAttribute(r, e[r]); return i; } function i(t, e) { t && (" " + t.className + " ").indexOf(" " + e + " ") === -1 && (t.className += (t.className ? " " : "") + e); } function r(t, i, r) { return "string" == typeof i && (i = e(i, r)), t.parentNode && t.parentNode.replaceChild(i, t), i.appendChild(t), i; } function n(t) { for (var e = 0, i = 0, r = fabric.document.documentElement, n = fabric.document.body || { scrollLeft: 0, scrollTop: 0 }; t && (t.parentNode || t.host) && (t = t.parentNode || t.host, t === fabric.document ? (e = n.scrollLeft || r.scrollLeft || 0, i = n.scrollTop || r.scrollTop || 0) : (e += t.scrollLeft || 0, i += t.scrollTop || 0), 1 !== t.nodeType || "fixed" !== fabric.util.getElementStyle(t, "position"));)
    ; return { left: e, top: i }; } function s(t) { var e, i, r = t && t.ownerDocument, s = { left: 0, top: 0 }, o = { left: 0, top: 0 }, a = { borderLeftWidth: "left", borderTopWidth: "top", paddingLeft: "left", paddingTop: "top" }; if (!r)
    return o; for (var h in a)
    o[a[h]] += parseInt(c(t, h), 10) || 0; return e = r.documentElement, "undefined" != typeof t.getBoundingClientRect && (s = t.getBoundingClientRect()), i = n(t), { left: s.left + i.left - (e.clientLeft || 0) + o.left, top: s.top + i.top - (e.clientTop || 0) + o.top }; } var o, a = Array.prototype.slice, h = function (t) { return a.call(t, 0); }; try {
    o = h(fabric.document.childNodes) instanceof Array;
}
catch (t) { } o || (h = function (t) { for (var e = new Array(t.length), i = t.length; i--;)
    e[i] = t[i]; return e; }); var c; c = fabric.document.defaultView && fabric.document.defaultView.getComputedStyle ? function (t, e) { var i = fabric.document.defaultView.getComputedStyle(t, null); return i ? i[e] : void 0; } : function (t, e) { var i = t.style[e]; return !i && t.currentStyle && (i = t.currentStyle[e]), i; }, function () { function t(t) { return "undefined" != typeof t.onselectstart && (t.onselectstart = fabric.util.falseFunction), r ? t.style[r] = "none" : "string" == typeof t.unselectable && (t.unselectable = "on"), t; } function e(t) { return "undefined" != typeof t.onselectstart && (t.onselectstart = null), r ? t.style[r] = "" : "string" == typeof t.unselectable && (t.unselectable = ""), t; } var i = fabric.document.documentElement.style, r = "userSelect" in i ? "userSelect" : "MozUserSelect" in i ? "MozUserSelect" : "WebkitUserSelect" in i ? "WebkitUserSelect" : "KhtmlUserSelect" in i ? "KhtmlUserSelect" : ""; fabric.util.makeElementUnselectable = t, fabric.util.makeElementSelectable = e; }(), function () { function t(t, e) { var i = fabric.document.getElementsByTagName("head")[0], r = fabric.document.createElement("script"), n = !0; r.onload = r.onreadystatechange = function (t) { if (n) {
    if ("string" == typeof this.readyState && "loaded" !== this.readyState && "complete" !== this.readyState)
        return;
    n = !1, e(t || fabric.window.event), r = r.onload = r.onreadystatechange = null;
} }, r.src = t, i.appendChild(r); } fabric.util.getScript = t; }(), fabric.util.getById = t, fabric.util.toArray = h, fabric.util.makeElement = e, fabric.util.addClass = i, fabric.util.wrapElement = r, fabric.util.getScrollLeftTop = n, fabric.util.getElementOffset = s, fabric.util.getElementStyle = c; }(), function () { function t(t, e) { return t + (/\?/.test(t) ? "&" : "?") + e; } function e() { } function i(i, n) { n || (n = {}); var s = n.method ? n.method.toUpperCase() : "GET", o = n.onComplete || function () { }, a = r(), h = n.body || n.parameters; return a.onreadystatechange = function () { 4 === a.readyState && (o(a), a.onreadystatechange = e); }, "GET" === s && (h = null, "string" == typeof n.parameters && (i = t(i, n.parameters))), a.open(s, i, !0), "POST" !== s && "PUT" !== s || a.setRequestHeader("Content-Type", "application/x-www-form-urlencoded"), a.send(h), a; } var r = function () { for (var t = [function () { return new ActiveXObject("Microsoft.XMLHTTP"); }, function () { return new ActiveXObject("Msxml2.XMLHTTP"); }, function () { return new ActiveXObject("Msxml2.XMLHTTP.3.0"); }, function () { return new XMLHttpRequest; }], e = t.length; e--;)
    try {
        var i = t[e]();
        if (i)
            return t[e];
    }
    catch (t) { } }(); fabric.util.request = i; }(), fabric.log = function () { }, fabric.warn = function () { }, "undefined" != typeof console && ["log", "warn"].forEach(function (t) { "undefined" != typeof console[t] && "function" == typeof console[t].apply && (fabric[t] = function () { return console[t].apply(console, arguments); }); }), function () { function t(t) { e(function (i) { t || (t = {}); var r, n = i || +new Date, s = t.duration || 500, o = n + s, a = t.onChange || function () { }, h = t.abort || function () { return !1; }, c = t.easing || function (t, e, i, r) { return -i * Math.cos(t / r * (Math.PI / 2)) + i + e; }, l = "startValue" in t ? t.startValue : 0, u = "endValue" in t ? t.endValue : 100, f = t.byValue || u - l; t.onStart && t.onStart(), function i(u) { r = u || +new Date; var d = r > o ? s : r - n; return h() ? void (t.onComplete && t.onComplete()) : (a(c(d, l, f, s)), r > o ? void (t.onComplete && t.onComplete()) : void e(i)); }(n); }); } function e() { return i.apply(fabric.window, arguments); } var i = fabric.window.requestAnimationFrame || fabric.window.webkitRequestAnimationFrame || fabric.window.mozRequestAnimationFrame || fabric.window.oRequestAnimationFrame || fabric.window.msRequestAnimationFrame || function (t) { fabric.window.setTimeout(t, 1e3 / 60); }; fabric.util.animate = t, fabric.util.requestAnimFrame = e; }(), function () { function t(t, e, i, r) { return t < Math.abs(e) ? (t = e, r = i / 4) : r = 0 === e && 0 === t ? i / (2 * Math.PI) * Math.asin(1) : i / (2 * Math.PI) * Math.asin(e / t), { a: t, c: e, p: i, s: r }; } function e(t, e, i) { return t.a * Math.pow(2, 10 * (e -= 1)) * Math.sin((e * i - t.s) * (2 * Math.PI) / t.p); } function i(t, e, i, r) { return i * ((t = t / r - 1) * t * t + 1) + e; } function r(t, e, i, r) { return t /= r / 2, t < 1 ? i / 2 * t * t * t + e : i / 2 * ((t -= 2) * t * t + 2) + e; } function n(t, e, i, r) { return i * (t /= r) * t * t * t + e; } function s(t, e, i, r) { return -i * ((t = t / r - 1) * t * t * t - 1) + e; } function o(t, e, i, r) { return t /= r / 2, t < 1 ? i / 2 * t * t * t * t + e : -i / 2 * ((t -= 2) * t * t * t - 2) + e; } function a(t, e, i, r) { return i * (t /= r) * t * t * t * t + e; } function h(t, e, i, r) { return i * ((t = t / r - 1) * t * t * t * t + 1) + e; } function c(t, e, i, r) { return t /= r / 2, t < 1 ? i / 2 * t * t * t * t * t + e : i / 2 * ((t -= 2) * t * t * t * t + 2) + e; } function l(t, e, i, r) { return -i * Math.cos(t / r * (Math.PI / 2)) + i + e; } function u(t, e, i, r) { return i * Math.sin(t / r * (Math.PI / 2)) + e; } function f(t, e, i, r) { return -i / 2 * (Math.cos(Math.PI * t / r) - 1) + e; } function d(t, e, i, r) { return 0 === t ? e : i * Math.pow(2, 10 * (t / r - 1)) + e; } function g(t, e, i, r) { return t === r ? e + i : i * (-Math.pow(2, -10 * t / r) + 1) + e; } function p(t, e, i, r) { return 0 === t ? e : t === r ? e + i : (t /= r / 2, t < 1 ? i / 2 * Math.pow(2, 10 * (t - 1)) + e : i / 2 * (-Math.pow(2, -10 * --t) + 2) + e); } function v(t, e, i, r) { return -i * (Math.sqrt(1 - (t /= r) * t) - 1) + e; } function b(t, e, i, r) { return i * Math.sqrt(1 - (t = t / r - 1) * t) + e; } function m(t, e, i, r) { return t /= r / 2, t < 1 ? -i / 2 * (Math.sqrt(1 - t * t) - 1) + e : i / 2 * (Math.sqrt(1 - (t -= 2) * t) + 1) + e; } function y(i, r, n, s) { var o = 1.70158, a = 0, h = n; if (0 === i)
    return r; if (i /= s, 1 === i)
    return r + n; a || (a = .3 * s); var c = t(h, n, a, o); return -e(c, i, s) + r; } function _(e, i, r, n) { var s = 1.70158, o = 0, a = r; if (0 === e)
    return i; if (e /= n, 1 === e)
    return i + r; o || (o = .3 * n); var h = t(a, r, o, s); return h.a * Math.pow(2, -10 * e) * Math.sin((e * n - h.s) * (2 * Math.PI) / h.p) + h.c + i; } function x(i, r, n, s) { var o = 1.70158, a = 0, h = n; if (0 === i)
    return r; if (i /= s / 2, 2 === i)
    return r + n; a || (a = s * (.3 * 1.5)); var c = t(h, n, a, o); return i < 1 ? -.5 * e(c, i, s) + r : c.a * Math.pow(2, -10 * (i -= 1)) * Math.sin((i * s - c.s) * (2 * Math.PI) / c.p) * .5 + c.c + r; } function S(t, e, i, r, n) { return void 0 === n && (n = 1.70158), i * (t /= r) * t * ((n + 1) * t - n) + e; } function C(t, e, i, r, n) { return void 0 === n && (n = 1.70158), i * ((t = t / r - 1) * t * ((n + 1) * t + n) + 1) + e; } function w(t, e, i, r, n) { return void 0 === n && (n = 1.70158), t /= r / 2, t < 1 ? i / 2 * (t * t * (((n *= 1.525) + 1) * t - n)) + e : i / 2 * ((t -= 2) * t * (((n *= 1.525) + 1) * t + n) + 2) + e; } function O(t, e, i, r) { return i - T(r - t, 0, i, r) + e; } function T(t, e, i, r) { return (t /= r) < 1 / 2.75 ? i * (7.5625 * t * t) + e : t < 2 / 2.75 ? i * (7.5625 * (t -= 1.5 / 2.75) * t + .75) + e : t < 2.5 / 2.75 ? i * (7.5625 * (t -= 2.25 / 2.75) * t + .9375) + e : i * (7.5625 * (t -= 2.625 / 2.75) * t + .984375) + e; } function k(t, e, i, r) { return t < r / 2 ? .5 * O(2 * t, 0, i, r) + e : .5 * T(2 * t - r, 0, i, r) + .5 * i + e; } fabric.util.ease = { easeInQuad: function (t, e, i, r) { return i * (t /= r) * t + e; }, easeOutQuad: function (t, e, i, r) { return -i * (t /= r) * (t - 2) + e; }, easeInOutQuad: function (t, e, i, r) { return t /= r / 2, t < 1 ? i / 2 * t * t + e : -i / 2 * (--t * (t - 2) - 1) + e; }, easeInCubic: function (t, e, i, r) { return i * (t /= r) * t * t + e; }, easeOutCubic: i, easeInOutCubic: r, easeInQuart: n, easeOutQuart: s, easeInOutQuart: o, easeInQuint: a, easeOutQuint: h, easeInOutQuint: c, easeInSine: l, easeOutSine: u, easeInOutSine: f, easeInExpo: d, easeOutExpo: g, easeInOutExpo: p, easeInCirc: v, easeOutCirc: b, easeInOutCirc: m, easeInElastic: y, easeOutElastic: _, easeInOutElastic: x, easeInBack: S, easeOutBack: C, easeInOutBack: w, easeInBounce: O, easeOutBounce: T, easeInOutBounce: k }; }(), function (t) {
    "use strict";
    function e(t) { return t in k ? k[t] : t; }
    function i(t, e, i, r) { var n, s = "[object Array]" === Object.prototype.toString.call(e); return "fill" !== t && "stroke" !== t || "none" !== e ? "strokeDashArray" === t ? e = e.replace(/,/g, " ").split(/\s+/).map(function (t) { return parseFloat(t); }) : "transformMatrix" === t ? e = i && i.transformMatrix ? S(i.transformMatrix, v.parseTransformAttribute(e)) : v.parseTransformAttribute(e) : "visible" === t ? (e = "none" !== e && "hidden" !== e, i && i.visible === !1 && (e = !1)) : "originX" === t ? e = "start" === e ? "left" : "end" === e ? "right" : "center" : n = s ? e.map(x) : x(e, r) : e = "", !s && isNaN(n) ? e : n; }
    function r(t) { for (var e in j)
        if ("undefined" != typeof t[j[e]] && "" !== t[e]) {
            if ("undefined" == typeof t[e]) {
                if (!v.Object.prototype[e])
                    continue;
                t[e] = v.Object.prototype[e];
            }
            if (0 !== t[e].indexOf("url(")) {
                var i = new v.Color(t[e]);
                t[e] = i.setAlpha(_(i.getAlpha() * t[j[e]], 2)).toRgba();
            }
        } return t; }
    function n(t, e) { for (var i, r, n = [], s = 0; s < e.length; s++)
        i = e[s], r = t.getElementsByTagName(i), n = n.concat(Array.prototype.slice.call(r)); return n; }
    function s(t, r) { var n, s; t.replace(/;\s*$/, "").split(";").forEach(function (t) { var o = t.split(":"); n = e(o[0].trim().toLowerCase()), s = i(n, o[1].trim()), r[n] = s; }); }
    function o(t, r) { var n, s; for (var o in t)
        "undefined" != typeof t[o] && (n = e(o.toLowerCase()), s = i(n, t[o]), r[n] = s); }
    function a(t, e) { var i = {}; for (var r in v.cssRules[e])
        if (h(t, r.split(" ")))
            for (var n in v.cssRules[e][r])
                i[n] = v.cssRules[e][r][n]; return i; }
    function h(t, e) { var i, r = !0; return i = l(t, e.pop()), i && e.length && (r = c(t, e)), i && r && 0 === e.length; }
    function c(t, e) { for (var i, r = !0; t.parentNode && 1 === t.parentNode.nodeType && e.length;)
        r && (i = e.pop()), t = t.parentNode, r = l(t, i); return 0 === e.length; }
    function l(t, e) { var i, r = t.nodeName, n = t.getAttribute("class"), s = t.getAttribute("id"); if (i = new RegExp("^" + r, "i"), e = e.replace(i, ""), s && e.length && (i = new RegExp("#" + s + "(?![a-zA-Z\\-]+)", "i"), e = e.replace(i, "")), n && e.length) {
        n = n.split(" ");
        for (var o = n.length; o--;)
            i = new RegExp("\\." + n[o] + "(?![a-zA-Z\\-]+)", "i"), e = e.replace(i, "");
    } return 0 === e.length; }
    function u(t, e) { var i; if (t.getElementById && (i = t.getElementById(e)), i)
        return i; var r, n, s = t.getElementsByTagName("*"); for (n = 0; n < s.length; n++)
        if (r = s[n], e === r.getAttribute("id"))
            return r; }
    function f(t) { for (var e = n(t, ["use", "svg:use"]), i = 0; e.length && i < e.length;) {
        var r, s, o, a, h, c = e[i], l = c.getAttribute("xlink:href").substr(1), f = c.getAttribute("x") || 0, g = c.getAttribute("y") || 0, p = u(t, l).cloneNode(!0), v = (p.getAttribute("transform") || "") + " translate(" + f + ", " + g + ")", b = e.length;
        if (d(p), /^svg$/i.test(p.nodeName)) {
            var m = p.ownerDocument.createElement("g");
            for (o = 0, a = p.attributes, h = a.length; o < h; o++)
                s = a.item(o), m.setAttribute(s.nodeName, s.nodeValue);
            for (; p.firstChild;)
                m.appendChild(p.firstChild);
            p = m;
        }
        for (o = 0, a = c.attributes, h = a.length; o < h; o++)
            s = a.item(o), "x" !== s.nodeName && "y" !== s.nodeName && "xlink:href" !== s.nodeName && ("transform" === s.nodeName ? v = s.nodeValue + " " + v : p.setAttribute(s.nodeName, s.nodeValue));
        p.setAttribute("transform", v), p.setAttribute("instantiated_by_use", "1"), p.removeAttribute("id"), r = c.parentNode, r.replaceChild(p, c), e.length === b && i++;
    } }
    function d(t) { var e, i, r, n, s = t.getAttribute("viewBox"), o = 1, a = 1, h = 0, c = 0, l = t.getAttribute("width"), u = t.getAttribute("height"), f = t.getAttribute("x") || 0, d = t.getAttribute("y") || 0, g = t.getAttribute("preserveAspectRatio") || "", p = !s || !w.test(t.nodeName) || !(s = s.match(M)), b = !l || !u || "100%" === l || "100%" === u, m = p && b, y = {}, _ = ""; if (y.width = 0, y.height = 0, y.toBeParsed = m, m)
        return y; if (p)
        return y.width = x(l), y.height = x(u), y; if (h = -parseFloat(s[1]), c = -parseFloat(s[2]), e = parseFloat(s[3]), i = parseFloat(s[4]), b ? (y.width = e, y.height = i) : (y.width = x(l), y.height = x(u), o = y.width / e, a = y.height / i), g = v.util.parsePreserveAspectRatioAttribute(g), "none" !== g.alignX && (a = o = o > a ? a : o), 1 === o && 1 === a && 0 === h && 0 === c && 0 === f && 0 === d)
        return y; if ((f || d) && (_ = " translate(" + x(f) + " " + x(d) + ") "), r = _ + " matrix(" + o + " 0 0 " + a + " " + h * o + " " + c * a + ") ", "svg" === t.nodeName) {
        for (n = t.ownerDocument.createElement("g"); t.firstChild;)
            n.appendChild(t.firstChild);
        t.appendChild(n);
    }
    else
        n = t, r = n.getAttribute("transform") + r; return n.setAttribute("transform", r), y; }
    function g(t) { var e = t.objects, i = t.options; return e = e.map(function (t) { return v[m(t.type)].fromObject(t); }), { objects: e, options: i }; }
    function p(t, e, i) { e[i] && e[i].toSVG && t.push('\t<pattern x="0" y="0" id="', i, 'Pattern" ', 'width="', e[i].source.width, '" height="', e[i].source.height, '" patternUnits="userSpaceOnUse">\n', '\t\t<image x="0" y="0" ', 'width="', e[i].source.width, '" height="', e[i].source.height, '" xlink:href="', e[i].source.src, '"></image>\n\t</pattern>\n'); }
    var v = t.fabric || (t.fabric = {}), b = v.util.object.extend, m = v.util.string.capitalize, y = v.util.object.clone, _ = v.util.toFixed, x = v.util.parseUnit, S = v.util.multiplyTransformMatrices, C = /^(path|circle|polygon|polyline|ellipse|rect|line|image|text)$/i, w = /^(symbol|image|marker|pattern|view|svg)$/i, O = /^(?:pattern|defs|symbol|metadata)$/i, T = /^(symbol|g|a|svg)$/i, k = { cx: "left", x: "left", r: "radius", cy: "top", y: "top", display: "visible", visibility: "visible", transform: "transformMatrix", "fill-opacity": "fillOpacity", "fill-rule": "fillRule", "font-family": "fontFamily", "font-size": "fontSize", "font-style": "fontStyle", "font-weight": "fontWeight", "stroke-dasharray": "strokeDashArray", "stroke-linecap": "strokeLineCap", "stroke-linejoin": "strokeLineJoin", "stroke-miterlimit": "strokeMiterLimit", "stroke-opacity": "strokeOpacity", "stroke-width": "strokeWidth", "text-decoration": "textDecoration", "text-anchor": "originX" }, j = { stroke: "strokeOpacity", fill: "fillOpacity" };
    v.cssRules = {}, v.gradientDefs = {}, v.parseTransformAttribute = function () {
        function t(t, e) { var i = e[0], r = 3 === e.length ? e[1] : 0, n = 3 === e.length ? e[2] : 0; t[0] = Math.cos(i), t[1] = Math.sin(i), t[2] = -Math.sin(i), t[3] = Math.cos(i), t[4] = r - (t[0] * r + t[2] * n), t[5] = n - (t[1] * r + t[3] * n); }
        function e(t, e) { var i = e[0], r = 2 === e.length ? e[1] : e[0]; t[0] = i, t[3] = r; }
        function i(t, e) {
            t[2] = Math.tan(v.util.degreesToRadians(e[0]));
        }
        function r(t, e) { t[1] = Math.tan(v.util.degreesToRadians(e[0])); }
        function n(t, e) { t[4] = e[0], 2 === e.length && (t[5] = e[1]); }
        var s = [1, 0, 0, 1, 0, 0], o = v.reNum, a = "(?:\\s+,?\\s*|,\\s*)", h = "(?:(skewX)\\s*\\(\\s*(" + o + ")\\s*\\))", c = "(?:(skewY)\\s*\\(\\s*(" + o + ")\\s*\\))", l = "(?:(rotate)\\s*\\(\\s*(" + o + ")(?:" + a + "(" + o + ")" + a + "(" + o + "))?\\s*\\))", u = "(?:(scale)\\s*\\(\\s*(" + o + ")(?:" + a + "(" + o + "))?\\s*\\))", f = "(?:(translate)\\s*\\(\\s*(" + o + ")(?:" + a + "(" + o + "))?\\s*\\))", d = "(?:(matrix)\\s*\\(\\s*(" + o + ")" + a + "(" + o + ")" + a + "(" + o + ")" + a + "(" + o + ")" + a + "(" + o + ")" + a + "(" + o + ")\\s*\\))", g = "(?:" + d + "|" + f + "|" + u + "|" + l + "|" + h + "|" + c + ")", p = "(?:" + g + "(?:" + a + "*" + g + ")*)", b = "^\\s*(?:" + p + "?)\\s*$", m = new RegExp(b), y = new RegExp(g, "g");
        return function (o) { var a = s.concat(), h = []; if (!o || o && !m.test(o))
            return a; o.replace(y, function (o) { var c = new RegExp(g).exec(o).filter(function (t) { return !!t; }), l = c[1], u = c.slice(2).map(parseFloat); switch (l) {
            case "translate":
                n(a, u);
                break;
            case "rotate":
                u[0] = v.util.degreesToRadians(u[0]), t(a, u);
                break;
            case "scale":
                e(a, u);
                break;
            case "skewX":
                i(a, u);
                break;
            case "skewY":
                r(a, u);
                break;
            case "matrix": a = u;
        } h.push(a.concat()), a = s.concat(); }); for (var c = h[0]; h.length > 1;)
            h.shift(), c = v.util.multiplyTransformMatrices(c, h[0]); return c; };
    }();
    var M = new RegExp("^\\s*(" + v.reNum + "+)\\s*,?\\s*(" + v.reNum + "+)\\s*,?\\s*(" + v.reNum + "+)\\s*,?\\s*(" + v.reNum + "+)\\s*$");
    v.parseSVGDocument = function () { function t(t, e) { for (; t && (t = t.parentNode);)
        if (t.nodeName && e.test(t.nodeName.replace("svg:", "")) && !t.getAttribute("instantiated_by_use"))
            return !0; return !1; } return function (e, i, r) { if (e) {
        f(e);
        var n = new Date, s = v.Object.__uid++, o = d(e), a = v.util.toArray(e.getElementsByTagName("*"));
        if (o.svgUid = s, 0 === a.length && v.isLikelyNode) {
            a = e.selectNodes('//*[name(.)!="svg"]');
            for (var h = [], c = 0, l = a.length; c < l; c++)
                h[c] = a[c];
            a = h;
        }
        var u = a.filter(function (e) { return d(e), C.test(e.nodeName.replace("svg:", "")) && !t(e, O); });
        if (!u || u && !u.length)
            return void (i && i([], {}));
        v.gradientDefs[s] = v.getGradientDefs(e), v.cssRules[s] = v.getCSSRules(e), v.parseElements(u, function (t) { v.documentParsingTime = new Date - n, i && i(t, o); }, y(o), r);
    } }; }();
    var A = { has: function (t, e) { e(!1); }, get: function () { }, set: function () { } }, P = new RegExp("(normal|italic)?\\s*(normal|small-caps)?\\s*(normal|bold|bolder|lighter|100|200|300|400|500|600|700|800|900)?\\s*(" + v.reNum + "(?:px|cm|mm|em|pt|pc|in)*)(?:\\/(normal|" + v.reNum + "))?\\s+(.*)");
    b(v, { parseFontDeclaration: function (t, e) { var i = t.match(P); if (i) {
            var r = i[1], n = i[3], s = i[4], o = i[5], a = i[6];
            r && (e.fontStyle = r), n && (e.fontWeight = isNaN(parseFloat(n)) ? n : parseFloat(n)), s && (e.fontSize = x(s)), a && (e.fontFamily = a), o && (e.lineHeight = "normal" === o ? 1 : o);
        } }, getGradientDefs: function (t) { var e, i, r, s = ["linearGradient", "radialGradient", "svg:linearGradient", "svg:radialGradient"], o = n(t, s), a = 0, h = {}, c = {}; for (a = o.length; a--;)
            e = o[a], r = e.getAttribute("xlink:href"), i = e.getAttribute("id"), r && (c[i] = r.substr(1)), h[i] = e; for (i in c) {
            var l = h[c[i]].cloneNode(!0);
            for (e = h[i]; l.firstChild;)
                e.appendChild(l.firstChild);
        } return h; }, parseAttributes: function (t, n, s) { if (t) {
            var o, h, c = {};
            "undefined" == typeof s && (s = t.getAttribute("svgUid")), t.parentNode && T.test(t.parentNode.nodeName) && (c = v.parseAttributes(t.parentNode, n, s)), h = c && c.fontSize || t.getAttribute("font-size") || v.Text.DEFAULT_SVG_FONT_SIZE;
            var l = n.reduce(function (r, n) { return o = t.getAttribute(n), o && (n = e(n), o = i(n, o, c, h), r[n] = o), r; }, {});
            return l = b(l, b(a(t, s), v.parseStyleAttribute(t))), l.font && v.parseFontDeclaration(l.font, l), r(b(c, l));
        } }, parseElements: function (t, e, i, r) { new v.ElementsParser(t, e, i, r).parse(); }, parseStyleAttribute: function (t) { var e = {}, i = t.getAttribute("style"); return i ? ("string" == typeof i ? s(i, e) : o(i, e), e) : e; }, parsePointsAttribute: function (t) { if (!t)
            return null; t = t.replace(/,/g, " ").trim(), t = t.split(/\s+/); var e, i, r = []; for (e = 0, i = t.length; e < i; e += 2)
            r.push({ x: parseFloat(t[e]), y: parseFloat(t[e + 1]) }); return r; }, getCSSRules: function (t) { for (var r, n = t.getElementsByTagName("style"), s = {}, o = 0, a = n.length; o < a; o++) {
            var h = n[o].textContent || n[o].text;
            h = h.replace(/\/\*[\s\S]*?\*\//g, ""), "" !== h.trim() && (r = h.match(/[^{]*\{[\s\S]*?\}/g), r = r.map(function (t) { return t.trim(); }), r.forEach(function (t) { for (var r = t.match(/([\s\S]*?)\s*\{([^}]*)\}/), n = {}, o = r[2].trim(), a = o.replace(/;$/, "").split(/\s*;\s*/), h = 0, c = a.length; h < c; h++) {
                var l = a[h].split(/\s*:\s*/), u = e(l[0]), f = i(u, l[1], l[0]);
                n[u] = f;
            } t = r[1], t.split(",").forEach(function (t) { t = t.replace(/^svg/i, "").trim(), "" !== t && (s[t] ? v.util.object.extend(s[t], n) : s[t] = v.util.object.clone(n)); }); }));
        } return s; }, loadSVGFromURL: function (t, e, i) { function r(r) { var n = r.responseXML; n && !n.documentElement && v.window.ActiveXObject && r.responseText && (n = new ActiveXObject("Microsoft.XMLDOM"), n.async = "false", n.loadXML(r.responseText.replace(/<!DOCTYPE[\s\S]*?(\[[\s\S]*\])*?>/i, ""))), n && n.documentElement || e && e(null), v.parseSVGDocument(n.documentElement, function (i, r) { A.set(t, { objects: v.util.array.invoke(i, "toObject"), options: r }), e && e(i, r); }, i); } t = t.replace(/^\n\s*/, "").trim(), A.has(t, function (i) { i ? A.get(t, function (t) { var i = g(t); e(i.objects, i.options); }) : new v.util.request(t, { method: "get", onComplete: r }); }); }, loadSVGFromString: function (t, e, i) { t = t.trim(); var r; if ("undefined" != typeof DOMParser) {
            var n = new DOMParser;
            n && n.parseFromString && (r = n.parseFromString(t, "text/xml"));
        }
        else
            v.window.ActiveXObject && (r = new ActiveXObject("Microsoft.XMLDOM"), r.async = "false", r.loadXML(t.replace(/<!DOCTYPE[\s\S]*?(\[[\s\S]*\])*?>/i, ""))); v.parseSVGDocument(r.documentElement, function (t, i) { e(t, i); }, i); }, createSVGFontFacesMarkup: function (t) { for (var e, i, r, n, s, o, a, h = "", c = {}, l = v.fontPaths, u = 0, f = t.length; u < f; u++)
            if (e = t[u], i = e.fontFamily, e.type.indexOf("text") !== -1 && !c[i] && l[i] && (c[i] = !0, e.styles)) {
                r = e.styles;
                for (s in r) {
                    n = r[s];
                    for (a in n)
                        o = n[a], i = o.fontFamily, !c[i] && l[i] && (c[i] = !0);
                }
            } for (var d in c)
            h += ["\t\t@font-face {\n", "\t\t\tfont-family: '", d, "';\n", "\t\t\tsrc: url('", l[d], "');\n", "\t\t}\n"].join(""); return h && (h = ['\t<style type="text/css">', "<![CDATA[\n", h, "]]>", "</style>\n"].join("")), h; }, createSVGRefElementsMarkup: function (t) { var e = []; return p(e, t, "backgroundColor"), p(e, t, "overlayColor"), e.join(""); } });
}("undefined" != typeof exports ? exports : this), fabric.ElementsParser = function (t, e, i, r) { this.elements = t, this.callback = e, this.options = i, this.reviver = r, this.svgUid = i && i.svgUid || 0; }, fabric.ElementsParser.prototype.parse = function () { this.instances = new Array(this.elements.length), this.numElements = this.elements.length, this.createObjects(); }, fabric.ElementsParser.prototype.createObjects = function () { for (var t = 0, e = this.elements.length; t < e; t++)
    this.elements[t].setAttribute("svgUid", this.svgUid), function (t, e) { setTimeout(function () { t.createObject(t.elements[e], e); }, 0); }(this, t); }, fabric.ElementsParser.prototype.createObject = function (t, e) { var i = fabric[fabric.util.string.capitalize(t.tagName.replace("svg:", ""))]; if (i && i.fromElement)
    try {
        this._createObject(i, t, e);
    }
    catch (t) {
        fabric.log(t);
    }
else
    this.checkIfDone(); }, fabric.ElementsParser.prototype._createObject = function (t, e, i) { if (t.async)
    t.fromElement(e, this.createCallback(i, e), this.options);
else {
    var r = t.fromElement(e, this.options);
    this.resolveGradient(r, "fill"), this.resolveGradient(r, "stroke"), this.reviver && this.reviver(e, r), this.instances[i] = r, this.checkIfDone();
} }, fabric.ElementsParser.prototype.createCallback = function (t, e) { var i = this; return function (r) { i.resolveGradient(r, "fill"), i.resolveGradient(r, "stroke"), i.reviver && i.reviver(e, r), i.instances[t] = r, i.checkIfDone(); }; }, fabric.ElementsParser.prototype.resolveGradient = function (t, e) { var i = t.get(e); if (/^url\(/.test(i)) {
    var r = i.slice(5, i.length - 1);
    fabric.gradientDefs[this.svgUid][r] && t.set(e, fabric.Gradient.fromElement(fabric.gradientDefs[this.svgUid][r], t));
} }, fabric.ElementsParser.prototype.checkIfDone = function () { 0 === --this.numElements && (this.instances = this.instances.filter(function (t) { return null != t; }), this.callback(this.instances)); }, function (t) {
    "use strict";
    function e(t, e) { this.x = t, this.y = e; }
    var i = t.fabric || (t.fabric = {});
    return i.Point ? void i.warn("fabric.Point is already defined") : (i.Point = e, void (e.prototype = { type: "point", constructor: e, add: function (t) { return new e(this.x + t.x, this.y + t.y); }, addEquals: function (t) { return this.x += t.x, this.y += t.y, this; }, scalarAdd: function (t) { return new e(this.x + t, this.y + t); }, scalarAddEquals: function (t) { return this.x += t, this.y += t, this; }, subtract: function (t) { return new e(this.x - t.x, this.y - t.y); }, subtractEquals: function (t) { return this.x -= t.x, this.y -= t.y, this; }, scalarSubtract: function (t) { return new e(this.x - t, this.y - t); }, scalarSubtractEquals: function (t) { return this.x -= t, this.y -= t, this; }, multiply: function (t) { return new e(this.x * t, this.y * t); }, multiplyEquals: function (t) { return this.x *= t, this.y *= t, this; }, divide: function (t) { return new e(this.x / t, this.y / t); }, divideEquals: function (t) { return this.x /= t, this.y /= t, this; }, eq: function (t) { return this.x === t.x && this.y === t.y; }, lt: function (t) { return this.x < t.x && this.y < t.y; }, lte: function (t) { return this.x <= t.x && this.y <= t.y; }, gt: function (t) { return this.x > t.x && this.y > t.y; }, gte: function (t) { return this.x >= t.x && this.y >= t.y; }, lerp: function (t, i) { return "undefined" == typeof i && (i = .5), i = Math.max(Math.min(1, i), 0), new e(this.x + (t.x - this.x) * i, this.y + (t.y - this.y) * i); }, distanceFrom: function (t) { var e = this.x - t.x, i = this.y - t.y; return Math.sqrt(e * e + i * i); }, midPointFrom: function (t) { return this.lerp(t); }, min: function (t) { return new e(Math.min(this.x, t.x), Math.min(this.y, t.y)); }, max: function (t) { return new e(Math.max(this.x, t.x), Math.max(this.y, t.y)); }, toString: function () { return this.x + "," + this.y; }, setXY: function (t, e) { return this.x = t, this.y = e, this; }, setX: function (t) { return this.x = t, this; }, setY: function (t) { return this.y = t, this; }, setFromPoint: function (t) { return this.x = t.x, this.y = t.y, this; }, swap: function (t) { var e = this.x, i = this.y; this.x = t.x, this.y = t.y, t.x = e, t.y = i; }, clone: function () { return new e(this.x, this.y); } }));
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    function e(t) { this.status = t, this.points = []; }
    var i = t.fabric || (t.fabric = {});
    return i.Intersection ? void i.warn("fabric.Intersection is already defined") : (i.Intersection = e, i.Intersection.prototype = { constructor: e, appendPoint: function (t) { return this.points.push(t), this; }, appendPoints: function (t) { return this.points = this.points.concat(t), this; } }, i.Intersection.intersectLineLine = function (t, r, n, s) { var o, a = (s.x - n.x) * (t.y - n.y) - (s.y - n.y) * (t.x - n.x), h = (r.x - t.x) * (t.y - n.y) - (r.y - t.y) * (t.x - n.x), c = (s.y - n.y) * (r.x - t.x) - (s.x - n.x) * (r.y - t.y); if (0 !== c) {
        var l = a / c, u = h / c;
        0 <= l && l <= 1 && 0 <= u && u <= 1 ? (o = new e("Intersection"), o.appendPoint(new i.Point(t.x + l * (r.x - t.x), t.y + l * (r.y - t.y)))) : o = new e;
    }
    else
        o = new e(0 === a || 0 === h ? "Coincident" : "Parallel"); return o; }, i.Intersection.intersectLinePolygon = function (t, i, r) { for (var n, s, o, a = new e, h = r.length, c = 0; c < h; c++)
        n = r[c], s = r[(c + 1) % h], o = e.intersectLineLine(t, i, n, s), a.appendPoints(o.points); return a.points.length > 0 && (a.status = "Intersection"), a; }, i.Intersection.intersectPolygonPolygon = function (t, i) { for (var r = new e, n = t.length, s = 0; s < n; s++) {
        var o = t[s], a = t[(s + 1) % n], h = e.intersectLinePolygon(o, a, i);
        r.appendPoints(h.points);
    } return r.points.length > 0 && (r.status = "Intersection"), r; }, void (i.Intersection.intersectPolygonRectangle = function (t, r, n) { var s = r.min(n), o = r.max(n), a = new i.Point(o.x, s.y), h = new i.Point(s.x, o.y), c = e.intersectLinePolygon(s, a, t), l = e.intersectLinePolygon(a, o, t), u = e.intersectLinePolygon(o, h, t), f = e.intersectLinePolygon(h, s, t), d = new e; return d.appendPoints(c.points), d.appendPoints(l.points), d.appendPoints(u.points), d.appendPoints(f.points), d.points.length > 0 && (d.status = "Intersection"), d; }));
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    function e(t) { t ? this._tryParsingColor(t) : this.setSource([0, 0, 0, 1]); }
    function i(t, e, i) { return i < 0 && (i += 1), i > 1 && (i -= 1), i < 1 / 6 ? t + 6 * (e - t) * i : i < .5 ? e : i < 2 / 3 ? t + (e - t) * (2 / 3 - i) * 6 : t; }
    var r = t.fabric || (t.fabric = {});
    return r.Color ? void r.warn("fabric.Color is already defined.") : (r.Color = e, r.Color.prototype = { _tryParsingColor: function (t) { var i; t in e.colorNameMap && (t = e.colorNameMap[t]), "transparent" === t && (i = [255, 255, 255, 0]), i || (i = e.sourceFromHex(t)), i || (i = e.sourceFromRgb(t)), i || (i = e.sourceFromHsl(t)), i || (i = [0, 0, 0, 1]), i && this.setSource(i); }, _rgbToHsl: function (t, e, i) { t /= 255, e /= 255, i /= 255; var n, s, o, a = r.util.array.max([t, e, i]), h = r.util.array.min([t, e, i]); if (o = (a + h) / 2, a === h)
            n = s = 0;
        else {
            var c = a - h;
            switch (s = o > .5 ? c / (2 - a - h) : c / (a + h), a) {
                case t:
                    n = (e - i) / c + (e < i ? 6 : 0);
                    break;
                case e:
                    n = (i - t) / c + 2;
                    break;
                case i: n = (t - e) / c + 4;
            }
            n /= 6;
        } return [Math.round(360 * n), Math.round(100 * s), Math.round(100 * o)]; }, getSource: function () { return this._source; }, setSource: function (t) { this._source = t; }, toRgb: function () { var t = this.getSource(); return "rgb(" + t[0] + "," + t[1] + "," + t[2] + ")"; }, toRgba: function () { var t = this.getSource(); return "rgba(" + t[0] + "," + t[1] + "," + t[2] + "," + t[3] + ")"; }, toHsl: function () { var t = this.getSource(), e = this._rgbToHsl(t[0], t[1], t[2]); return "hsl(" + e[0] + "," + e[1] + "%," + e[2] + "%)"; }, toHsla: function () { var t = this.getSource(), e = this._rgbToHsl(t[0], t[1], t[2]); return "hsla(" + e[0] + "," + e[1] + "%," + e[2] + "%," + t[3] + ")"; }, toHex: function () { var t, e, i, r = this.getSource(); return t = r[0].toString(16), t = 1 === t.length ? "0" + t : t, e = r[1].toString(16), e = 1 === e.length ? "0" + e : e, i = r[2].toString(16), i = 1 === i.length ? "0" + i : i, t.toUpperCase() + e.toUpperCase() + i.toUpperCase(); }, getAlpha: function () { return this.getSource()[3]; }, setAlpha: function (t) { var e = this.getSource(); return e[3] = t, this.setSource(e), this; }, toGrayscale: function () { var t = this.getSource(), e = parseInt((.3 * t[0] + .59 * t[1] + .11 * t[2]).toFixed(0), 10), i = t[3]; return this.setSource([e, e, e, i]), this; }, toBlackWhite: function (t) { var e = this.getSource(), i = (.3 * e[0] + .59 * e[1] + .11 * e[2]).toFixed(0), r = e[3]; return t = t || 127, i = Number(i) < Number(t) ? 0 : 255, this.setSource([i, i, i, r]), this; }, overlayWith: function (t) { t instanceof e || (t = new e(t)); for (var i = [], r = this.getAlpha(), n = .5, s = this.getSource(), o = t.getSource(), a = 0; a < 3; a++)
            i.push(Math.round(s[a] * (1 - n) + o[a] * n)); return i[3] = r, this.setSource(i), this; } }, r.Color.reRGBa = /^rgba?\(\s*(\d{1,3}(?:\.\d+)?\%?)\s*,\s*(\d{1,3}(?:\.\d+)?\%?)\s*,\s*(\d{1,3}(?:\.\d+)?\%?)\s*(?:\s*,\s*(\d+(?:\.\d+)?)\s*)?\)$/, r.Color.reHSLa = /^hsla?\(\s*(\d{1,3})\s*,\s*(\d{1,3}\%)\s*,\s*(\d{1,3}\%)\s*(?:\s*,\s*(\d+(?:\.\d+)?)\s*)?\)$/, r.Color.reHex = /^#?([0-9a-f]{8}|[0-9a-f]{6}|[0-9a-f]{4}|[0-9a-f]{3})$/i, r.Color.colorNameMap = { aqua: "#00FFFF", black: "#000000", blue: "#0000FF", fuchsia: "#FF00FF", gray: "#808080", grey: "#808080", green: "#008000", lime: "#00FF00", maroon: "#800000", navy: "#000080", olive: "#808000", orange: "#FFA500", purple: "#800080", red: "#FF0000", silver: "#C0C0C0", teal: "#008080", white: "#FFFFFF", yellow: "#FFFF00" }, r.Color.fromRgb = function (t) { return e.fromSource(e.sourceFromRgb(t)); }, r.Color.sourceFromRgb = function (t) { var i = t.match(e.reRGBa); if (i) {
        var r = parseInt(i[1], 10) / (/%$/.test(i[1]) ? 100 : 1) * (/%$/.test(i[1]) ? 255 : 1), n = parseInt(i[2], 10) / (/%$/.test(i[2]) ? 100 : 1) * (/%$/.test(i[2]) ? 255 : 1), s = parseInt(i[3], 10) / (/%$/.test(i[3]) ? 100 : 1) * (/%$/.test(i[3]) ? 255 : 1);
        return [parseInt(r, 10), parseInt(n, 10), parseInt(s, 10), i[4] ? parseFloat(i[4]) : 1];
    } }, r.Color.fromRgba = e.fromRgb, r.Color.fromHsl = function (t) { return e.fromSource(e.sourceFromHsl(t)); }, r.Color.sourceFromHsl = function (t) { var r = t.match(e.reHSLa); if (r) {
        var n, s, o, a = (parseFloat(r[1]) % 360 + 360) % 360 / 360, h = parseFloat(r[2]) / (/%$/.test(r[2]) ? 100 : 1), c = parseFloat(r[3]) / (/%$/.test(r[3]) ? 100 : 1);
        if (0 === h)
            n = s = o = c;
        else {
            var l = c <= .5 ? c * (h + 1) : c + h - c * h, u = 2 * c - l;
            n = i(u, l, a + 1 / 3), s = i(u, l, a), o = i(u, l, a - 1 / 3);
        }
        return [Math.round(255 * n), Math.round(255 * s), Math.round(255 * o), r[4] ? parseFloat(r[4]) : 1];
    } }, r.Color.fromHsla = e.fromHsl, r.Color.fromHex = function (t) { return e.fromSource(e.sourceFromHex(t)); }, r.Color.sourceFromHex = function (t) { if (t.match(e.reHex)) {
        var i = t.slice(t.indexOf("#") + 1), r = 3 === i.length || 4 === i.length, n = 8 === i.length || 4 === i.length, s = r ? i.charAt(0) + i.charAt(0) : i.substring(0, 2), o = r ? i.charAt(1) + i.charAt(1) : i.substring(2, 4), a = r ? i.charAt(2) + i.charAt(2) : i.substring(4, 6), h = n ? r ? i.charAt(3) + i.charAt(3) : i.substring(6, 8) : "FF";
        return [parseInt(s, 16), parseInt(o, 16), parseInt(a, 16), parseFloat((parseInt(h, 16) / 255).toFixed(2))];
    } }, void (r.Color.fromSource = function (t) { var i = new e; return i.setSource(t), i; }));
}("undefined" != typeof exports ? exports : this), function () { function t(t) { var e, i, r, n = t.getAttribute("style"), s = t.getAttribute("offset") || 0; if (s = parseFloat(s) / (/%$/.test(s) ? 100 : 1), s = s < 0 ? 0 : s > 1 ? 1 : s, n) {
    var o = n.split(/\s*;\s*/);
    "" === o[o.length - 1] && o.pop();
    for (var a = o.length; a--;) {
        var h = o[a].split(/\s*:\s*/), c = h[0].trim(), l = h[1].trim();
        "stop-color" === c ? e = l : "stop-opacity" === c && (r = l);
    }
} return e || (e = t.getAttribute("stop-color") || "rgb(0,0,0)"), r || (r = t.getAttribute("stop-opacity")), e = new fabric.Color(e), i = e.getAlpha(), r = isNaN(parseFloat(r)) ? 1 : parseFloat(r), r *= i, { offset: s, color: e.toRgb(), opacity: r }; } function e(t) { return { x1: t.getAttribute("x1") || 0, y1: t.getAttribute("y1") || 0, x2: t.getAttribute("x2") || "100%", y2: t.getAttribute("y2") || 0 }; } function i(t) { return { x1: t.getAttribute("fx") || t.getAttribute("cx") || "50%", y1: t.getAttribute("fy") || t.getAttribute("cy") || "50%", r1: 0, x2: t.getAttribute("cx") || "50%", y2: t.getAttribute("cy") || "50%", r2: t.getAttribute("r") || "50%" }; } function r(t, e, i) { var r, n = 0, s = 1, o = ""; for (var a in e)
    "Infinity" === e[a] ? e[a] = 1 : "-Infinity" === e[a] && (e[a] = 0), r = parseFloat(e[a], 10), s = "string" == typeof e[a] && /^\d+%$/.test(e[a]) ? .01 : 1, "x1" === a || "x2" === a || "r2" === a ? (s *= "objectBoundingBox" === i ? t.width : 1, n = "objectBoundingBox" === i ? t.left || 0 : 0) : "y1" !== a && "y2" !== a || (s *= "objectBoundingBox" === i ? t.height : 1, n = "objectBoundingBox" === i ? t.top || 0 : 0), e[a] = r * s + n; if ("ellipse" === t.type && null !== e.r2 && "objectBoundingBox" === i && t.rx !== t.ry) {
    var h = t.ry / t.rx;
    o = " scale(1, " + h + ")", e.y1 && (e.y1 /= h), e.y2 && (e.y2 /= h);
} return o; } fabric.Gradient = fabric.util.createClass({ offsetX: 0, offsetY: 0, initialize: function (t) { t || (t = {}); var e = {}; this.id = fabric.Object.__uid++, this.type = t.type || "linear", e = { x1: t.coords.x1 || 0, y1: t.coords.y1 || 0, x2: t.coords.x2 || 0, y2: t.coords.y2 || 0 }, "radial" === this.type && (e.r1 = t.coords.r1 || 0, e.r2 = t.coords.r2 || 0), this.coords = e, this.colorStops = t.colorStops.slice(), t.gradientTransform && (this.gradientTransform = t.gradientTransform), this.offsetX = t.offsetX || this.offsetX, this.offsetY = t.offsetY || this.offsetY; }, addColorStop: function (t) { for (var e in t) {
        var i = new fabric.Color(t[e]);
        this.colorStops.push({ offset: e, color: i.toRgb(), opacity: i.getAlpha() });
    } return this; }, toObject: function () { return { type: this.type, coords: this.coords, colorStops: this.colorStops, offsetX: this.offsetX, offsetY: this.offsetY, gradientTransform: this.gradientTransform ? this.gradientTransform.concat() : this.gradientTransform }; }, toSVG: function (t) { var e, i, r = fabric.util.object.clone(this.coords); if (this.colorStops.sort(function (t, e) { return t.offset - e.offset; }), !t.group || "path-group" !== t.group.type)
        for (var n in r)
            "x1" === n || "x2" === n || "r2" === n ? r[n] += this.offsetX - t.width / 2 : "y1" !== n && "y2" !== n || (r[n] += this.offsetY - t.height / 2); i = 'id="SVGID_' + this.id + '" gradientUnits="userSpaceOnUse"', this.gradientTransform && (i += ' gradientTransform="matrix(' + this.gradientTransform.join(" ") + ')" '), "linear" === this.type ? e = ["<linearGradient ", i, ' x1="', r.x1, '" y1="', r.y1, '" x2="', r.x2, '" y2="', r.y2, '">\n'] : "radial" === this.type && (e = ["<radialGradient ", i, ' cx="', r.x2, '" cy="', r.y2, '" r="', r.r2, '" fx="', r.x1, '" fy="', r.y1, '">\n']); for (var s = 0; s < this.colorStops.length; s++)
        e.push("<stop ", 'offset="', 100 * this.colorStops[s].offset + "%", '" style="stop-color:', this.colorStops[s].color, null !== this.colorStops[s].opacity ? ";stop-opacity: " + this.colorStops[s].opacity : ";", '"/>\n'); return e.push("linear" === this.type ? "</linearGradient>\n" : "</radialGradient>\n"), e.join(""); }, toLive: function (t, e) { var i, r, n = fabric.util.object.clone(this.coords); if (this.type) {
        if (e.group && "path-group" === e.group.type)
            for (r in n)
                "x1" === r || "x2" === r ? n[r] += -this.offsetX + e.width / 2 : "y1" !== r && "y2" !== r || (n[r] += -this.offsetY + e.height / 2);
        "linear" === this.type ? i = t.createLinearGradient(n.x1, n.y1, n.x2, n.y2) : "radial" === this.type && (i = t.createRadialGradient(n.x1, n.y1, n.r1, n.x2, n.y2, n.r2));
        for (var s = 0, o = this.colorStops.length; s < o; s++) {
            var a = this.colorStops[s].color, h = this.colorStops[s].opacity, c = this.colorStops[s].offset;
            "undefined" != typeof h && (a = new fabric.Color(a).setAlpha(h).toRgba()), i.addColorStop(parseFloat(c), a);
        }
        return i;
    } } }), fabric.util.object.extend(fabric.Gradient, { fromElement: function (n, s) { var o, a, h, c = n.getElementsByTagName("stop"), l = n.getAttribute("gradientUnits") || "objectBoundingBox", u = n.getAttribute("gradientTransform"), f = []; o = "linearGradient" === n.nodeName || "LINEARGRADIENT" === n.nodeName ? "linear" : "radial", "linear" === o ? a = e(n) : "radial" === o && (a = i(n)); for (var d = c.length; d--;)
        f.push(t(c[d])); h = r(s, a, l); var g = new fabric.Gradient({ type: o, coords: a, colorStops: f, offsetX: -s.left, offsetY: -s.top }); return (u || "" !== h) && (g.gradientTransform = fabric.parseTransformAttribute((u || "") + h)), g; }, forObject: function (t, e) { return e || (e = {}), r(t, e.coords, "userSpaceOnUse"), new fabric.Gradient(e); } }); }(), fabric.Pattern = fabric.util.createClass({ repeat: "repeat", offsetX: 0, offsetY: 0, initialize: function (t) { if (t || (t = {}), this.id = fabric.Object.__uid++, t.source)
        if ("string" == typeof t.source)
            if ("undefined" != typeof fabric.util.getFunctionBody(t.source))
                this.source = new Function(fabric.util.getFunctionBody(t.source));
            else {
                var e = this;
                this.source = fabric.util.createImage(), fabric.util.loadImage(t.source, function (t) { e.source = t; });
            }
        else
            this.source = t.source; t.repeat && (this.repeat = t.repeat), t.offsetX && (this.offsetX = t.offsetX), t.offsetY && (this.offsetY = t.offsetY); }, toObject: function () { var t; return "function" == typeof this.source ? t = String(this.source) : "string" == typeof this.source.src ? t = this.source.src : "object" == typeof this.source && this.source.toDataURL && (t = this.source.toDataURL()), { source: t, repeat: this.repeat, offsetX: this.offsetX, offsetY: this.offsetY }; }, toSVG: function (t) { var e = "function" == typeof this.source ? this.source() : this.source, i = e.width / t.getWidth(), r = e.height / t.getHeight(), n = this.offsetX / t.getWidth(), s = this.offsetY / t.getHeight(), o = ""; return "repeat-x" !== this.repeat && "no-repeat" !== this.repeat || (r = 1), "repeat-y" !== this.repeat && "no-repeat" !== this.repeat || (i = 1), e.src ? o = e.src : e.toDataURL && (o = e.toDataURL()), '<pattern id="SVGID_' + this.id + '" x="' + n + '" y="' + s + '" width="' + i + '" height="' + r + '">\n<image x="0" y="0" width="' + e.width + '" height="' + e.height + '" xlink:href="' + o + '"></image>\n</pattern>\n'; }, toLive: function (t) { var e = "function" == typeof this.source ? this.source() : this.source; if (!e)
        return ""; if ("undefined" != typeof e.src) {
        if (!e.complete)
            return "";
        if (0 === e.naturalWidth || 0 === e.naturalHeight)
            return "";
    } return t.createPattern(e, this.repeat); } }), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = e.util.toFixed;
    return e.Shadow ? void e.warn("fabric.Shadow is already defined.") : (e.Shadow = e.util.createClass({ color: "rgb(0,0,0)", blur: 0, offsetX: 0, offsetY: 0, affectStroke: !1, includeDefaultValues: !0, initialize: function (t) { "string" == typeof t && (t = this._parseShadow(t)); for (var i in t)
            this[i] = t[i]; this.id = e.Object.__uid++; }, _parseShadow: function (t) { var i = t.trim(), r = e.Shadow.reOffsetsAndBlur.exec(i) || [], n = i.replace(e.Shadow.reOffsetsAndBlur, "") || "rgb(0,0,0)"; return { color: n.trim(), offsetX: parseInt(r[1], 10) || 0, offsetY: parseInt(r[2], 10) || 0, blur: parseInt(r[3], 10) || 0 }; }, toString: function () { return [this.offsetX, this.offsetY, this.blur, this.color].join("px "); }, toSVG: function (t) { var r = 40, n = 40, s = e.Object.NUM_FRACTION_DIGITS, o = e.util.rotateVector({ x: this.offsetX, y: this.offsetY }, e.util.degreesToRadians(-t.angle)), a = 20; return t.width && t.height && (r = 100 * i((Math.abs(o.x) + this.blur) / t.width, s) + a, n = 100 * i((Math.abs(o.y) + this.blur) / t.height, s) + a), t.flipX && (o.x *= -1), t.flipY && (o.y *= -1), '<filter id="SVGID_' + this.id + '" y="-' + n + '%" height="' + (100 + 2 * n) + '%" x="-' + r + '%" width="' + (100 + 2 * r) + '%" >\n\t<feGaussianBlur in="SourceAlpha" stdDeviation="' + i(this.blur ? this.blur / 2 : 0, s) + '"></feGaussianBlur>\n\t<feOffset dx="' + i(o.x, s) + '" dy="' + i(o.y, s) + '" result="oBlur" ></feOffset>\n\t<feFlood flood-color="' + this.color + '"/>\n\t<feComposite in2="oBlur" operator="in" />\n\t<feMerge>\n\t\t<feMergeNode></feMergeNode>\n\t\t<feMergeNode in="SourceGraphic"></feMergeNode>\n\t</feMerge>\n</filter>\n'; }, toObject: function () { if (this.includeDefaultValues)
            return { color: this.color, blur: this.blur, offsetX: this.offsetX, offsetY: this.offsetY, affectStroke: this.affectStroke }; var t = {}, i = e.Shadow.prototype; return ["color", "blur", "offsetX", "offsetY", "affectStroke"].forEach(function (e) { this[e] !== i[e] && (t[e] = this[e]); }, this), t; } }), void (e.Shadow.reOffsetsAndBlur = /(?:\s|^)(-?\d+(?:px)?(?:\s?|$))?(-?\d+(?:px)?(?:\s?|$))?(\d+(?:px)?)?(?:\s?|$)(?:$|\s)/));
}("undefined" != typeof exports ? exports : this), function () {
    "use strict";
    if (fabric.StaticCanvas)
        return void fabric.warn("fabric.StaticCanvas is already defined.");
    var t = fabric.util.object.extend, e = fabric.util.getElementOffset, i = fabric.util.removeFromArray, r = fabric.util.toFixed, n = new Error("Could not initialize `canvas` element");
    fabric.StaticCanvas = fabric.util.createClass({ initialize: function (t, e) { e || (e = {}), this._initStatic(t, e); }, backgroundColor: "", backgroundImage: null, overlayColor: "", overlayImage: null, includeDefaultValues: !0, stateful: !0, renderOnAddRemove: !0, clipTo: null, controlsAboveOverlay: !1, allowTouchScrolling: !1, imageSmoothingEnabled: !0, viewportTransform: [1, 0, 0, 1, 0, 0], backgroundVpt: !0, overlayVpt: !0, onBeforeScaleRotate: function () { }, enableRetinaScaling: !0, _initStatic: function (t, e) { var i = fabric.StaticCanvas.prototype.renderAll.bind(this); this._objects = [], this._createLowerCanvas(t), this._initOptions(e), this._setImageSmoothing(), this.interactive || this._initRetinaScaling(), e.overlayImage && this.setOverlayImage(e.overlayImage, i), e.backgroundImage && this.setBackgroundImage(e.backgroundImage, i), e.backgroundColor && this.setBackgroundColor(e.backgroundColor, i), e.overlayColor && this.setOverlayColor(e.overlayColor, i), this.calcOffset(); }, _isRetinaScaling: function () { return 1 !== fabric.devicePixelRatio && this.enableRetinaScaling; }, getRetinaScaling: function () { return this._isRetinaScaling() ? fabric.devicePixelRatio : 1; }, _initRetinaScaling: function () { this._isRetinaScaling() && (this.lowerCanvasEl.setAttribute("width", this.width * fabric.devicePixelRatio), this.lowerCanvasEl.setAttribute("height", this.height * fabric.devicePixelRatio), this.contextContainer.scale(fabric.devicePixelRatio, fabric.devicePixelRatio)); }, calcOffset: function () { return this._offset = e(this.lowerCanvasEl), this; }, setOverlayImage: function (t, e, i) { return this.__setBgOverlayImage("overlayImage", t, e, i); }, setBackgroundImage: function (t, e, i) { return this.__setBgOverlayImage("backgroundImage", t, e, i); }, setOverlayColor: function (t, e) { return this.__setBgOverlayColor("overlayColor", t, e); }, setBackgroundColor: function (t, e) { return this.__setBgOverlayColor("backgroundColor", t, e); }, _setImageSmoothing: function () { var t = this.getContext(); t.imageSmoothingEnabled = t.imageSmoothingEnabled || t.webkitImageSmoothingEnabled || t.mozImageSmoothingEnabled || t.msImageSmoothingEnabled || t.oImageSmoothingEnabled, t.imageSmoothingEnabled = this.imageSmoothingEnabled; }, __setBgOverlayImage: function (t, e, i, r) { return "string" == typeof e ? fabric.util.loadImage(e, function (e) { e && (this[t] = new fabric.Image(e, r)), i && i(e); }, this, r && r.crossOrigin) : (r && e.setOptions(r), this[t] = e, i && i(e)), this; }, __setBgOverlayColor: function (t, e, i) { if (e && e.source) {
            var r = this;
            fabric.util.loadImage(e.source, function (n) { r[t] = new fabric.Pattern({ source: n, repeat: e.repeat, offsetX: e.offsetX, offsetY: e.offsetY }), i && i(); });
        }
        else
            this[t] = e, i && i(); return this; }, _createCanvasElement: function (t) { var e = fabric.util.createCanvasElement(t); if (e.style || (e.style = {}), !e)
            throw n; if ("undefined" == typeof e.getContext)
            throw n; return e; }, _initOptions: function (t) { for (var e in t)
            this[e] = t[e]; this.width = this.width || parseInt(this.lowerCanvasEl.width, 10) || 0, this.height = this.height || parseInt(this.lowerCanvasEl.height, 10) || 0, this.lowerCanvasEl.style && (this.lowerCanvasEl.width = this.width, this.lowerCanvasEl.height = this.height, this.lowerCanvasEl.style.width = this.width + "px", this.lowerCanvasEl.style.height = this.height + "px", this.viewportTransform = this.viewportTransform.slice()); }, _createLowerCanvas: function (t) { this.lowerCanvasEl = fabric.util.getById(t) || this._createCanvasElement(t), fabric.util.addClass(this.lowerCanvasEl, "lower-canvas"), this.interactive && this._applyCanvasStyle(this.lowerCanvasEl), this.contextContainer = this.lowerCanvasEl.getContext("2d"); }, getWidth: function () { return this.width; }, getHeight: function () { return this.height; }, setWidth: function (t, e) { return this.setDimensions({ width: t }, e); }, setHeight: function (t, e) { return this.setDimensions({ height: t }, e); }, setDimensions: function (t, e) { var i; e = e || {}; for (var r in t)
            i = t[r], e.cssOnly || (this._setBackstoreDimension(r, t[r]), i += "px"), e.backstoreOnly || this._setCssDimension(r, i); return this._initRetinaScaling(), this._setImageSmoothing(), this.calcOffset(), e.cssOnly || this.renderAll(), this; }, _setBackstoreDimension: function (t, e) { return this.lowerCanvasEl[t] = e, this.upperCanvasEl && (this.upperCanvasEl[t] = e), this.cacheCanvasEl && (this.cacheCanvasEl[t] = e), this[t] = e, this; }, _setCssDimension: function (t, e) { return this.lowerCanvasEl.style[t] = e, this.upperCanvasEl && (this.upperCanvasEl.style[t] = e), this.wrapperEl && (this.wrapperEl.style[t] = e), this; }, getZoom: function () { return Math.sqrt(this.viewportTransform[0] * this.viewportTransform[3]); }, setViewportTransform: function (t) { var e, i = this._activeGroup; this.viewportTransform = t; for (var r = 0, n = this._objects.length; r < n; r++)
            e = this._objects[r], e.group || e.setCoords(); return i && i.setCoords(), this.renderAll(), this; }, zoomToPoint: function (t, e) { var i = t, r = this.viewportTransform.slice(0); t = fabric.util.transformPoint(t, fabric.util.invertTransform(this.viewportTransform)), r[0] = e, r[3] = e; var n = fabric.util.transformPoint(t, r); return r[4] += i.x - n.x, r[5] += i.y - n.y, this.setViewportTransform(r); }, setZoom: function (t) { return this.zoomToPoint(new fabric.Point(0, 0), t), this; }, absolutePan: function (t) { var e = this.viewportTransform.slice(0); return e[4] = -t.x, e[5] = -t.y, this.setViewportTransform(e); }, relativePan: function (t) { return this.absolutePan(new fabric.Point(-t.x - this.viewportTransform[4], -t.y - this.viewportTransform[5])); }, getElement: function () { return this.lowerCanvasEl; }, _onObjectAdded: function (t) { this.stateful && t.setupState(), t._set("canvas", this), t.setCoords(), this.fire("object:added", { target: t }), t.fire("added"); }, _onObjectRemoved: function (t) { this.fire("object:removed", { target: t }), t.fire("removed"), delete t.canvas; }, clearContext: function (t) { return t.clearRect(0, 0, this.width, this.height), this; }, getContext: function () { return this.contextContainer; }, clear: function () { return this._objects.length = 0, this.backgroundImage = null, this.overlayImage = null, this.backgroundColor = "", this.overlayColor = "", this._hasITextHandlers && (this.off("selection:cleared", this._canvasITextSelectionClearedHanlder), this.off("object:selected", this._canvasITextSelectionClearedHanlder), this.off("mouse:up", this._mouseUpITextHandler), this._iTextInstances = null, this._hasITextHandlers = !1), this.clearContext(this.contextContainer), this.fire("canvas:cleared"), this.renderAll(), this; }, renderAll: function () { var t = this.contextContainer; return this.renderCanvas(t, this._objects), this; }, renderCanvas: function (t, e) { this.clearContext(t), this.fire("before:render"), this.clipTo && fabric.util.clipContext(this, t), this._renderBackground(t), t.save(), t.transform.apply(t, this.viewportTransform), this._renderObjects(t, e), t.restore(), !this.controlsAboveOverlay && this.interactive && this.drawControls(t), this.clipTo && t.restore(), this._renderOverlay(t), this.controlsAboveOverlay && this.interactive && this.drawControls(t), this.fire("after:render"); }, _renderObjects: function (t, e) { for (var i = 0, r = e.length; i < r; ++i)
            e[i] && e[i].render(t); }, _renderBackgroundOrOverlay: function (t, e) { var i = this[e + "Color"]; i && (t.fillStyle = i.toLive ? i.toLive(t) : i, t.fillRect(i.offsetX || 0, i.offsetY || 0, this.width, this.height)), i = this[e + "Image"], i && (this[e + "Vpt"] && (t.save(), t.transform.apply(t, this.viewportTransform)), i.render(t), this[e + "Vpt"] && t.restore()); }, _renderBackground: function (t) { this._renderBackgroundOrOverlay(t, "background"); }, _renderOverlay: function (t) { this._renderBackgroundOrOverlay(t, "overlay"); }, getCenter: function () { return { top: this.getHeight() / 2, left: this.getWidth() / 2 }; }, centerObjectH: function (t) { return this._centerObject(t, new fabric.Point(this.getCenter().left, t.getCenterPoint().y)); }, centerObjectV: function (t) { return this._centerObject(t, new fabric.Point(t.getCenterPoint().x, this.getCenter().top)); }, centerObject: function (t) { var e = this.getCenter(); return this._centerObject(t, new fabric.Point(e.left, e.top)); }, viewportCenterObject: function (t) { var e = this.getVpCenter(); return this._centerObject(t, e); }, viewportCenterObjectH: function (t) {
            var e = this.getVpCenter();
            return this._centerObject(t, new fabric.Point(e.x, t.getCenterPoint().y)),
                this;
        }, viewportCenterObjectV: function (t) { var e = this.getVpCenter(); return this._centerObject(t, new fabric.Point(t.getCenterPoint().x, e.y)); }, getVpCenter: function () { var t = this.getCenter(), e = fabric.util.invertTransform(this.viewportTransform); return fabric.util.transformPoint({ x: t.left, y: t.top }, e); }, _centerObject: function (t, e) { return t.setPositionByOrigin(e, "center", "center"), this.renderAll(), this; }, toDatalessJSON: function (t) { return this.toDatalessObject(t); }, toObject: function (t) { return this._toObjectMethod("toObject", t); }, toDatalessObject: function (t) { return this._toObjectMethod("toDatalessObject", t); }, _toObjectMethod: function (e, i) { var r = { objects: this._toObjects(e, i) }; return t(r, this.__serializeBgOverlay(i)), fabric.util.populateWithProperties(this, r, i), r; }, _toObjects: function (t, e) { return this.getObjects().filter(function (t) { return !t.excludeFromExport; }).map(function (i) { return this._toObject(i, t, e); }, this); }, _toObject: function (t, e, i) { var r; this.includeDefaultValues || (r = t.includeDefaultValues, t.includeDefaultValues = !1); var n = t[e](i); return this.includeDefaultValues || (t.includeDefaultValues = r), n; }, __serializeBgOverlay: function (t) { var e = { background: this.backgroundColor && this.backgroundColor.toObject ? this.backgroundColor.toObject(t) : this.backgroundColor }; return this.overlayColor && (e.overlay = this.overlayColor.toObject ? this.overlayColor.toObject(t) : this.overlayColor), this.backgroundImage && (e.backgroundImage = this.backgroundImage.toObject(t)), this.overlayImage && (e.overlayImage = this.overlayImage.toObject(t)), e; }, svgViewportTransformation: !0, toSVG: function (t, e) { t || (t = {}); var i = []; return this._setSVGPreamble(i, t), this._setSVGHeader(i, t), this._setSVGBgOverlayColor(i, "backgroundColor"), this._setSVGBgOverlayImage(i, "backgroundImage", e), this._setSVGObjects(i, e), this._setSVGBgOverlayColor(i, "overlayColor"), this._setSVGBgOverlayImage(i, "overlayImage", e), i.push("</svg>"), i.join(""); }, _setSVGPreamble: function (t, e) { e.suppressPreamble || t.push('<?xml version="1.0" encoding="', e.encoding || "UTF-8", '" standalone="no" ?>\n', '<!DOCTYPE svg PUBLIC "-//W3C//DTD SVG 1.1//EN" ', '"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd">\n'); }, _setSVGHeader: function (t, e) { var i, n = e.width || this.width, s = e.height || this.height, o = 'viewBox="0 0 ' + this.width + " " + this.height + '" ', a = fabric.Object.NUM_FRACTION_DIGITS; e.viewBox ? o = 'viewBox="' + e.viewBox.x + " " + e.viewBox.y + " " + e.viewBox.width + " " + e.viewBox.height + '" ' : this.svgViewportTransformation && (i = this.viewportTransform, o = 'viewBox="' + r(-i[4] / i[0], a) + " " + r(-i[5] / i[3], a) + " " + r(this.width / i[0], a) + " " + r(this.height / i[3], a) + '" '), t.push("<svg ", 'xmlns="http://www.w3.org/2000/svg" ', 'xmlns:xlink="http://www.w3.org/1999/xlink" ', 'version="1.1" ', 'width="', n, '" ', 'height="', s, '" ', this.backgroundColor && !this.backgroundColor.toLive ? 'style="background-color: ' + this.backgroundColor + '" ' : null, o, 'xml:space="preserve">\n', "<desc>Created with Fabric.js ", fabric.version, "</desc>\n", "<defs>", fabric.createSVGFontFacesMarkup(this.getObjects()), fabric.createSVGRefElementsMarkup(this), "</defs>\n"); }, _setSVGObjects: function (t, e) { for (var i, r = 0, n = this.getObjects(), s = n.length; r < s; r++)
            i = n[r], i.excludeFromExport || this._setSVGObject(t, i, e); }, _setSVGObject: function (t, e, i) { t.push(e.toSVG(i)); }, _setSVGBgOverlayImage: function (t, e, i) { this[e] && this[e].toSVG && t.push(this[e].toSVG(i)); }, _setSVGBgOverlayColor: function (t, e) { this[e] && this[e].source ? t.push('<rect x="', this[e].offsetX, '" y="', this[e].offsetY, '" ', 'width="', "repeat-y" === this[e].repeat || "no-repeat" === this[e].repeat ? this[e].source.width : this.width, '" height="', "repeat-x" === this[e].repeat || "no-repeat" === this[e].repeat ? this[e].source.height : this.height, '" fill="url(#' + e + 'Pattern)"', "></rect>\n") : this[e] && "overlayColor" === e && t.push('<rect x="0" y="0" ', 'width="', this.width, '" height="', this.height, '" fill="', this[e], '"', "></rect>\n"); }, sendToBack: function (t) { if (!t)
            return this; var e, r, n, s = this._activeGroup; if (t === s)
            for (n = s._objects, e = n.length; e--;)
                r = n[e], i(this._objects, r), this._objects.unshift(r);
        else
            i(this._objects, t), this._objects.unshift(t); return this.renderAll && this.renderAll(); }, bringToFront: function (t) { if (!t)
            return this; var e, r, n, s = this._activeGroup; if (t === s)
            for (n = s._objects, e = 0; e < n.length; e++)
                r = n[e], i(this._objects, r), this._objects.push(r);
        else
            i(this._objects, t), this._objects.push(t); return this.renderAll && this.renderAll(); }, sendBackwards: function (t, e) { if (!t)
            return this; var r, n, s, o, a, h = this._activeGroup; if (t === h)
            for (a = h._objects, r = 0; r < a.length; r++)
                n = a[r], s = this._objects.indexOf(n), 0 !== s && (o = s - 1, i(this._objects, n), this._objects.splice(o, 0, n));
        else
            s = this._objects.indexOf(t), 0 !== s && (o = this._findNewLowerIndex(t, s, e), i(this._objects, t), this._objects.splice(o, 0, t)); return this.renderAll && this.renderAll(), this; }, _findNewLowerIndex: function (t, e, i) { var r; if (i) {
            r = e;
            for (var n = e - 1; n >= 0; --n) {
                var s = t.intersectsWithObject(this._objects[n]) || t.isContainedWithinObject(this._objects[n]) || this._objects[n].isContainedWithinObject(t);
                if (s) {
                    r = n;
                    break;
                }
            }
        }
        else
            r = e - 1; return r; }, bringForward: function (t, e) { if (!t)
            return this; var r, n, s, o, a, h = this._activeGroup; if (t === h)
            for (a = h._objects, r = a.length; r--;)
                n = a[r], s = this._objects.indexOf(n), s !== this._objects.length - 1 && (o = s + 1, i(this._objects, n), this._objects.splice(o, 0, n));
        else
            s = this._objects.indexOf(t), s !== this._objects.length - 1 && (o = this._findNewUpperIndex(t, s, e), i(this._objects, t), this._objects.splice(o, 0, t)); return this.renderAll && this.renderAll(), this; }, _findNewUpperIndex: function (t, e, i) { var r; if (i) {
            r = e;
            for (var n = e + 1; n < this._objects.length; ++n) {
                var s = t.intersectsWithObject(this._objects[n]) || t.isContainedWithinObject(this._objects[n]) || this._objects[n].isContainedWithinObject(t);
                if (s) {
                    r = n;
                    break;
                }
            }
        }
        else
            r = e + 1; return r; }, moveTo: function (t, e) { return i(this._objects, t), this._objects.splice(e, 0, t), this.renderAll && this.renderAll(); }, dispose: function () { return this.clear(), this; }, toString: function () { return "#<fabric.Canvas (" + this.complexity() + "): { objects: " + this.getObjects().length + " }>"; } }), t(fabric.StaticCanvas.prototype, fabric.Observable), t(fabric.StaticCanvas.prototype, fabric.Collection), t(fabric.StaticCanvas.prototype, fabric.DataURLExporter), t(fabric.StaticCanvas, { EMPTY_JSON: '{"objects": [], "background": "white"}', supports: function (t) { var e = fabric.util.createCanvasElement(); if (!e || !e.getContext)
            return null; var i = e.getContext("2d"); if (!i)
            return null; switch (t) {
            case "getImageData": return "undefined" != typeof i.getImageData;
            case "setLineDash": return "undefined" != typeof i.setLineDash;
            case "toDataURL": return "undefined" != typeof e.toDataURL;
            case "toDataURLWithQuality":
                try {
                    return e.toDataURL("image/jpeg", 0), !0;
                }
                catch (t) { }
                return !1;
            default: return null;
        } } }), fabric.StaticCanvas.prototype.toJSON = fabric.StaticCanvas.prototype.toObject;
}(), fabric.BaseBrush = fabric.util.createClass({ color: "rgb(0, 0, 0)", width: 1, shadow: null, strokeLineCap: "round", strokeLineJoin: "round", strokeDashArray: null, setShadow: function (t) { return this.shadow = new fabric.Shadow(t), this; }, _setBrushStyles: function () { var t = this.canvas.contextTop; t.strokeStyle = this.color, t.lineWidth = this.width, t.lineCap = this.strokeLineCap, t.lineJoin = this.strokeLineJoin, this.strokeDashArray && fabric.StaticCanvas.supports("setLineDash") && t.setLineDash(this.strokeDashArray); }, _setShadow: function () { if (this.shadow) {
        var t = this.canvas.contextTop;
        t.shadowColor = this.shadow.color, t.shadowBlur = this.shadow.blur, t.shadowOffsetX = this.shadow.offsetX, t.shadowOffsetY = this.shadow.offsetY;
    } }, _resetShadow: function () { var t = this.canvas.contextTop; t.shadowColor = "", t.shadowBlur = t.shadowOffsetX = t.shadowOffsetY = 0; } }), function () { fabric.PencilBrush = fabric.util.createClass(fabric.BaseBrush, { initialize: function (t) { this.canvas = t, this._points = []; }, onMouseDown: function (t) { this._prepareForDrawing(t), this._captureDrawingPath(t), this._render(); }, onMouseMove: function (t) { this._captureDrawingPath(t), this.canvas.clearContext(this.canvas.contextTop), this._render(); }, onMouseUp: function () { this._finalizeAndAddPath(); }, _prepareForDrawing: function (t) { var e = new fabric.Point(t.x, t.y); this._reset(), this._addPoint(e), this.canvas.contextTop.moveTo(e.x, e.y); }, _addPoint: function (t) { this._points.push(t); }, _reset: function () { this._points.length = 0, this._setBrushStyles(), this._setShadow(); }, _captureDrawingPath: function (t) { var e = new fabric.Point(t.x, t.y); this._addPoint(e); }, _render: function () { var t = this.canvas.contextTop, e = this.canvas.viewportTransform, i = this._points[0], r = this._points[1]; t.save(), t.transform(e[0], e[1], e[2], e[3], e[4], e[5]), t.beginPath(), 2 === this._points.length && i.x === r.x && i.y === r.y && (i.x -= .5, r.x += .5), t.moveTo(i.x, i.y); for (var n = 1, s = this._points.length; n < s; n++) {
        var o = i.midPointFrom(r);
        t.quadraticCurveTo(i.x, i.y, o.x, o.y), i = this._points[n], r = this._points[n + 1];
    } t.lineTo(i.x, i.y), t.stroke(), t.restore(); }, convertPointsToSVGPath: function (t) { var e = [], i = new fabric.Point(t[0].x, t[0].y), r = new fabric.Point(t[1].x, t[1].y); e.push("M ", t[0].x, " ", t[0].y, " "); for (var n = 1, s = t.length; n < s; n++) {
        var o = i.midPointFrom(r);
        e.push("Q ", i.x, " ", i.y, " ", o.x, " ", o.y, " "), i = new fabric.Point(t[n].x, t[n].y), n + 1 < t.length && (r = new fabric.Point(t[n + 1].x, t[n + 1].y));
    } return e.push("L ", i.x, " ", i.y, " "), e; }, createPath: function (t) { var e = new fabric.Path(t, { fill: null, stroke: this.color, strokeWidth: this.width, strokeLineCap: this.strokeLineCap, strokeLineJoin: this.strokeLineJoin, strokeDashArray: this.strokeDashArray, originX: "center", originY: "center" }); return this.shadow && (this.shadow.affectStroke = !0, e.setShadow(this.shadow)), e; }, _finalizeAndAddPath: function () { var t = this.canvas.contextTop; t.closePath(); var e = this.convertPointsToSVGPath(this._points).join(""); if ("M 0 0 Q 0 0 0 0 L 0 0" === e)
        return void this.canvas.renderAll(); var i = this.createPath(e); this.canvas.add(i), i.setCoords(), this.canvas.clearContext(this.canvas.contextTop), this._resetShadow(), this.canvas.renderAll(), this.canvas.fire("path:created", { path: i }); } }); }(), fabric.CircleBrush = fabric.util.createClass(fabric.BaseBrush, { width: 10, initialize: function (t) { this.canvas = t, this.points = []; }, drawDot: function (t) { var e = this.addPoint(t), i = this.canvas.contextTop, r = this.canvas.viewportTransform; i.save(), i.transform(r[0], r[1], r[2], r[3], r[4], r[5]), i.fillStyle = e.fill, i.beginPath(), i.arc(e.x, e.y, e.radius, 0, 2 * Math.PI, !1), i.closePath(), i.fill(), i.restore(); }, onMouseDown: function (t) { this.points.length = 0, this.canvas.clearContext(this.canvas.contextTop), this._setShadow(), this.drawDot(t); }, onMouseMove: function (t) { this.drawDot(t); }, onMouseUp: function () { var t = this.canvas.renderOnAddRemove; this.canvas.renderOnAddRemove = !1; for (var e = [], i = 0, r = this.points.length; i < r; i++) {
        var n = this.points[i], s = new fabric.Circle({ radius: n.radius, left: n.x, top: n.y, originX: "center", originY: "center", fill: n.fill });
        this.shadow && s.setShadow(this.shadow), e.push(s);
    } var o = new fabric.Group(e, { originX: "center", originY: "center" }); o.canvas = this.canvas, this.canvas.add(o), this.canvas.fire("path:created", { path: o }), this.canvas.clearContext(this.canvas.contextTop), this._resetShadow(), this.canvas.renderOnAddRemove = t, this.canvas.renderAll(); }, addPoint: function (t) { var e = new fabric.Point(t.x, t.y), i = fabric.util.getRandomInt(Math.max(0, this.width - 20), this.width + 20) / 2, r = new fabric.Color(this.color).setAlpha(fabric.util.getRandomInt(0, 100) / 100).toRgba(); return e.radius = i, e.fill = r, this.points.push(e), e; } }), fabric.SprayBrush = fabric.util.createClass(fabric.BaseBrush, { width: 10, density: 20, dotWidth: 1, dotWidthVariance: 1, randomOpacity: !1, optimizeOverlapping: !0, initialize: function (t) { this.canvas = t, this.sprayChunks = []; }, onMouseDown: function (t) { this.sprayChunks.length = 0, this.canvas.clearContext(this.canvas.contextTop), this._setShadow(), this.addSprayChunk(t), this.render(); }, onMouseMove: function (t) { this.addSprayChunk(t), this.render(); }, onMouseUp: function () { var t = this.canvas.renderOnAddRemove; this.canvas.renderOnAddRemove = !1; for (var e = [], i = 0, r = this.sprayChunks.length; i < r; i++)
        for (var n = this.sprayChunks[i], s = 0, o = n.length; s < o; s++) {
            var a = new fabric.Rect({ width: n[s].width, height: n[s].width, left: n[s].x + 1, top: n[s].y + 1, originX: "center", originY: "center", fill: this.color });
            this.shadow && a.setShadow(this.shadow), e.push(a);
        } this.optimizeOverlapping && (e = this._getOptimizedRects(e)); var h = new fabric.Group(e, { originX: "center", originY: "center" }); h.canvas = this.canvas, this.canvas.add(h), this.canvas.fire("path:created", { path: h }), this.canvas.clearContext(this.canvas.contextTop), this._resetShadow(), this.canvas.renderOnAddRemove = t, this.canvas.renderAll(); }, _getOptimizedRects: function (t) { for (var e, i = {}, r = 0, n = t.length; r < n; r++)
        e = t[r].left + "" + t[r].top, i[e] || (i[e] = t[r]); var s = []; for (e in i)
        s.push(i[e]); return s; }, render: function () { var t = this.canvas.contextTop; t.fillStyle = this.color; var e = this.canvas.viewportTransform; t.save(), t.transform(e[0], e[1], e[2], e[3], e[4], e[5]); for (var i = 0, r = this.sprayChunkPoints.length; i < r; i++) {
        var n = this.sprayChunkPoints[i];
        "undefined" != typeof n.opacity && (t.globalAlpha = n.opacity), t.fillRect(n.x, n.y, n.width, n.width);
    } t.restore(); }, addSprayChunk: function (t) { this.sprayChunkPoints = []; for (var e, i, r, n = this.width / 2, s = 0; s < this.density; s++) {
        e = fabric.util.getRandomInt(t.x - n, t.x + n), i = fabric.util.getRandomInt(t.y - n, t.y + n), r = this.dotWidthVariance ? fabric.util.getRandomInt(Math.max(1, this.dotWidth - this.dotWidthVariance), this.dotWidth + this.dotWidthVariance) : this.dotWidth;
        var o = new fabric.Point(e, i);
        o.width = r, this.randomOpacity && (o.opacity = fabric.util.getRandomInt(0, 100) / 100), this.sprayChunkPoints.push(o);
    } this.sprayChunks.push(this.sprayChunkPoints); } }), fabric.PatternBrush = fabric.util.createClass(fabric.PencilBrush, { getPatternSrc: function () { var t = 20, e = 5, i = fabric.document.createElement("canvas"), r = i.getContext("2d"); return i.width = i.height = t + e, r.fillStyle = this.color, r.beginPath(), r.arc(t / 2, t / 2, t / 2, 0, 2 * Math.PI, !1), r.closePath(), r.fill(), i; }, getPatternSrcFunction: function () { return String(this.getPatternSrc).replace("this.color", '"' + this.color + '"'); }, getPattern: function () { return this.canvas.contextTop.createPattern(this.source || this.getPatternSrc(), "repeat"); }, _setBrushStyles: function () { this.callSuper("_setBrushStyles"), this.canvas.contextTop.strokeStyle = this.getPattern(); }, createPath: function (t) { var e = this.callSuper("createPath", t), i = e._getLeftTopCoords().scalarAdd(e.strokeWidth / 2); return e.stroke = new fabric.Pattern({ source: this.source || this.getPatternSrcFunction(), offsetX: -i.x, offsetY: -i.y }), e; } }), function () { var t = fabric.util.getPointer, e = fabric.util.degreesToRadians, i = fabric.util.radiansToDegrees, r = Math.atan2, n = Math.abs, s = fabric.StaticCanvas.supports("setLineDash"), o = .5; fabric.Canvas = fabric.util.createClass(fabric.StaticCanvas, { initialize: function (t, e) { e || (e = {}), this._initStatic(t, e), this._initInteractive(), this._createCacheCanvas(); }, uniScaleTransform: !1, uniScaleKey: "shiftKey", centeredScaling: !1, centeredRotation: !1, centeredKey: "altKey", altActionKey: "shiftKey", interactive: !0, selection: !0, selectionKey: "shiftKey", altSelectionKey: null, selectionColor: "rgba(100, 100, 255, 0.3)", selectionDashArray: [], selectionBorderColor: "rgba(255, 255, 255, 0.3)", selectionLineWidth: 1, hoverCursor: "move", moveCursor: "move", defaultCursor: "default", freeDrawingCursor: "crosshair", rotationCursor: "crosshair", containerClass: "canvas-container", perPixelTargetFind: !1, targetFindTolerance: 0, skipTargetFind: !1, isDrawingMode: !1, preserveObjectStacking: !1, stopContextMenu: !1, fireRightClick: !1, _initInteractive: function () { this._currentTransform = null, this._groupSelector = null, this._initWrapperElement(), this._createUpperCanvas(), this._initEventListeners(), this._initRetinaScaling(), this.freeDrawingBrush = fabric.PencilBrush && new fabric.PencilBrush(this), this.calcOffset(); }, _chooseObjectsToRender: function () { var t, e = this.getActiveGroup(), i = this.getActiveObject(), r = [], n = []; if (!e && !i || this.preserveObjectStacking)
        r = this._objects;
    else {
        for (var s = 0, o = this._objects.length; s < o; s++)
            t = this._objects[s], e && e.contains(t) || t === i ? n.push(t) : r.push(t);
        e && (e._set("_objects", n), r.push(e)), i && r.push(i);
    } return r; }, renderAll: function () { !this.selection || this._groupSelector || this.isDrawingMode || this.clearContext(this.contextTop); var t = this.contextContainer; return this.renderCanvas(t, this._chooseObjectsToRender()), this; }, renderTop: function () { var t = this.contextTop; return this.clearContext(t), this.selection && this._groupSelector && this._drawSelection(t), this.fire("after:render"), this; }, _resetCurrentTransform: function () { var t = this._currentTransform; t.target.set({ scaleX: t.original.scaleX, scaleY: t.original.scaleY, skewX: t.original.skewX, skewY: t.original.skewY, left: t.original.left, top: t.original.top }), this._shouldCenterTransform(t.target) ? "rotate" === t.action ? this._setOriginToCenter(t.target) : ("center" !== t.originX && ("right" === t.originX ? t.mouseXSign = -1 : t.mouseXSign = 1), "center" !== t.originY && ("bottom" === t.originY ? t.mouseYSign = -1 : t.mouseYSign = 1), t.originX = "center", t.originY = "center") : (t.originX = t.original.originX, t.originY = t.original.originY); }, containsPoint: function (t, e, i) { var r, n = !0, s = i || this.getPointer(t, n); return r = e.group && e.group === this.getActiveGroup() ? this._normalizePointer(e.group, s) : { x: s.x, y: s.y }, e.containsPoint(r) || e._findTargetCorner(s); }, _normalizePointer: function (t, e) { var i = t.calcTransformMatrix(), r = fabric.util.invertTransform(i), n = this.viewportTransform, s = this.restorePointerVpt(e), o = fabric.util.transformPoint(s, r); return fabric.util.transformPoint(o, n); }, isTargetTransparent: function (t, e, i) { var r = t.hasBorders, n = t.transparentCorners, s = this.contextCache, o = t.selectionBackgroundColor; t.hasBorders = t.transparentCorners = !1, t.selectionBackgroundColor = "", s.save(), s.transform.apply(s, this.viewportTransform), t.render(s), s.restore(), t.active && t._renderControls(s), t.hasBorders = r, t.transparentCorners = n, t.selectionBackgroundColor = o; var a = fabric.util.isTransparent(s, e, i, this.targetFindTolerance); return this.clearContext(s), a; }, _shouldClearSelection: function (t, e) { var i = this.getActiveGroup(), r = this.getActiveObject(); return !e || e && i && !i.contains(e) && i !== e && !t[this.selectionKey] || e && !e.evented || e && !e.selectable && r && r !== e; }, _shouldCenterTransform: function (t) { if (t) {
        var e, i = this._currentTransform;
        return "scale" === i.action || "scaleX" === i.action || "scaleY" === i.action ? e = this.centeredScaling || t.centeredScaling : "rotate" === i.action && (e = this.centeredRotation || t.centeredRotation), e ? !i.altKey : i.altKey;
    } }, _getOriginFromCorner: function (t, e) { var i = { x: t.originX, y: t.originY }; return "ml" === e || "tl" === e || "bl" === e ? i.x = "right" : "mr" !== e && "tr" !== e && "br" !== e || (i.x = "left"), "tl" === e || "mt" === e || "tr" === e ? i.y = "bottom" : "bl" !== e && "mb" !== e && "br" !== e || (i.y = "top"), i; }, _getActionFromCorner: function (t, e, i) { if (!e)
        return "drag"; switch (e) {
        case "mtr": return "rotate";
        case "ml":
        case "mr": return i[this.altActionKey] ? "skewY" : "scaleX";
        case "mt":
        case "mb": return i[this.altActionKey] ? "skewX" : "scaleY";
        default: return "scale";
    } }, _setupCurrentTransform: function (t, i) { if (i) {
        var r = this.getPointer(t), n = i._findTargetCorner(this.getPointer(t, !0)), s = this._getActionFromCorner(i, n, t), o = this._getOriginFromCorner(i, n);
        this._currentTransform = { target: i, action: s, corner: n, scaleX: i.scaleX, scaleY: i.scaleY, skewX: i.skewX, skewY: i.skewY, offsetX: r.x - i.left, offsetY: r.y - i.top, originX: o.x, originY: o.y, ex: r.x, ey: r.y, lastX: r.x, lastY: r.y, left: i.left, top: i.top, theta: e(i.angle), width: i.width * i.scaleX, mouseXSign: 1, mouseYSign: 1, shiftKey: t.shiftKey, altKey: t[this.centeredKey] }, this._currentTransform.original = { left: i.left, top: i.top, scaleX: i.scaleX, scaleY: i.scaleY, skewX: i.skewX, skewY: i.skewY, originX: o.x, originY: o.y }, this._resetCurrentTransform();
    } }, _translateObject: function (t, e) { var i = this._currentTransform, r = i.target, n = t - i.offsetX, s = e - i.offsetY, o = !r.get("lockMovementX") && r.left !== n, a = !r.get("lockMovementY") && r.top !== s; return o && r.set("left", n), a && r.set("top", s), o || a; }, _changeSkewTransformOrigin: function (t, e, i) { var r = "originX", n = { 0: "center" }, s = e.target.skewX, o = "left", a = "right", h = "mt" === e.corner || "ml" === e.corner ? 1 : -1, c = 1; t = t > 0 ? 1 : -1, "y" === i && (s = e.target.skewY, o = "top", a = "bottom", r = "originY"), n[-1] = o, n[1] = a, e.target.flipX && (c *= -1), e.target.flipY && (c *= -1), 0 === s ? (e.skewSign = -h * t * c, e[r] = n[-t]) : (s = s > 0 ? 1 : -1, e.skewSign = s, e[r] = n[s * h * c]); }, _skewObject: function (t, e, i) { var r = this._currentTransform, n = r.target, s = !1, o = n.get("lockSkewingX"), a = n.get("lockSkewingY"); if (o && "x" === i || a && "y" === i)
        return !1; var h, c, l = n.getCenterPoint(), u = n.toLocalPoint(new fabric.Point(t, e), "center", "center")[i], f = n.toLocalPoint(new fabric.Point(r.lastX, r.lastY), "center", "center")[i], d = n._getTransformedDimensions(); return this._changeSkewTransformOrigin(u - f, r, i), h = n.toLocalPoint(new fabric.Point(t, e), r.originX, r.originY)[i], c = n.translateToOriginPoint(l, r.originX, r.originY), s = this._setObjectSkew(h, r, i, d), r.lastX = t, r.lastY = e, n.setPositionByOrigin(c, r.originX, r.originY), s; }, _setObjectSkew: function (t, e, i, r) { var n, s, o, a, h, c, l, u, f, d = e.target, g = !1, p = e.skewSign; return "x" === i ? (a = "y", h = "Y", c = "X", u = 0, f = d.skewY) : (a = "x", h = "X", c = "Y", u = d.skewX, f = 0), o = d._getTransformedDimensions(u, f), l = 2 * Math.abs(t) - o[i], l <= 2 ? n = 0 : (n = p * Math.atan(l / d["scale" + c] / (o[a] / d["scale" + h])), n = fabric.util.radiansToDegrees(n)), g = d["skew" + c] !== n, d.set("skew" + c, n), 0 !== d["skew" + h] && (s = d._getTransformedDimensions(), n = r[a] / s[a] * d["scale" + h], d.set("scale" + h, n)), g; }, _scaleObject: function (t, e, i) { var r = this._currentTransform, n = r.target, s = n.get("lockScalingX"), o = n.get("lockScalingY"), a = n.get("lockScalingFlip"); if (s && o)
        return !1; var h = n.translateToOriginPoint(n.getCenterPoint(), r.originX, r.originY), c = n.toLocalPoint(new fabric.Point(t, e), r.originX, r.originY), l = n._getTransformedDimensions(), u = !1; return this._setLocalMouse(c, r), u = this._setObjectScale(c, r, s, o, i, a, l), n.setPositionByOrigin(h, r.originX, r.originY), u; }, _setObjectScale: function (t, e, i, r, n, s, o) { var a, h, c, l, u = e.target, f = !1, d = !1, g = !1; return c = t.x * u.scaleX / o.x, l = t.y * u.scaleY / o.y, a = u.scaleX !== c, h = u.scaleY !== l, s && c <= 0 && c < u.scaleX && (f = !0), s && l <= 0 && l < u.scaleY && (d = !0), "equally" !== n || i || r ? n ? "x" !== n || u.get("lockUniScaling") ? "y" !== n || u.get("lockUniScaling") || d || r || u.set("scaleY", l) && (g = g || h) : f || i || u.set("scaleX", c) && (g = g || a) : (f || i || u.set("scaleX", c) && (g = g || a), d || r || u.set("scaleY", l) && (g = g || h)) : f || d || (g = this._scaleObjectEqually(t, u, e, o)), e.newScaleX = c, e.newScaleY = l, f || d || this._flipObject(e, n), g; }, _scaleObjectEqually: function (t, e, i, r) { var n, s = t.y + t.x, o = r.y * i.original.scaleY / e.scaleY + r.x * i.original.scaleX / e.scaleX; return i.newScaleX = i.original.scaleX * s / o, i.newScaleY = i.original.scaleY * s / o, n = i.newScaleX !== e.scaleX || i.newScaleY !== e.scaleY, e.set("scaleX", i.newScaleX), e.set("scaleY", i.newScaleY), n; }, _flipObject: function (t, e) { t.newScaleX < 0 && "y" !== e && ("left" === t.originX ? t.originX = "right" : "right" === t.originX && (t.originX = "left")), t.newScaleY < 0 && "x" !== e && ("top" === t.originY ? t.originY = "bottom" : "bottom" === t.originY && (t.originY = "top")); }, _setLocalMouse: function (t, e) { var i = e.target; "right" === e.originX ? t.x *= -1 : "center" === e.originX && (t.x *= 2 * e.mouseXSign, t.x < 0 && (e.mouseXSign = -e.mouseXSign)), "bottom" === e.originY ? t.y *= -1 : "center" === e.originY && (t.y *= 2 * e.mouseYSign, t.y < 0 && (e.mouseYSign = -e.mouseYSign)), n(t.x) > i.padding ? t.x < 0 ? t.x += i.padding : t.x -= i.padding : t.x = 0, n(t.y) > i.padding ? t.y < 0 ? t.y += i.padding : t.y -= i.padding : t.y = 0; }, _rotateObject: function (t, e) { var n = this._currentTransform; if (n.target.get("lockRotation"))
        return !1; var s = r(n.ey - n.top, n.ex - n.left), o = r(e - n.top, t - n.left), a = i(o - s + n.theta); return a < 0 && (a = 360 + a), n.target.angle = a % 360, !0; }, setCursor: function (t) { this.upperCanvasEl.style.cursor = t; }, _resetObjectTransform: function (t) { t.scaleX = 1, t.scaleY = 1, t.skewX = 0, t.skewY = 0, t.setAngle(0); }, _drawSelection: function (t) { var e = this._groupSelector, i = e.left, r = e.top, a = n(i), h = n(r); if (this.selectionColor && (t.fillStyle = this.selectionColor, t.fillRect(e.ex - (i > 0 ? 0 : -i), e.ey - (r > 0 ? 0 : -r), a, h)), this.selectionLineWidth && this.selectionBorderColor)
        if (t.lineWidth = this.selectionLineWidth, t.strokeStyle = this.selectionBorderColor, this.selectionDashArray.length > 1 && !s) {
            var c = e.ex + o - (i > 0 ? 0 : a), l = e.ey + o - (r > 0 ? 0 : h);
            t.beginPath(), fabric.util.drawDashedLine(t, c, l, c + a, l, this.selectionDashArray), fabric.util.drawDashedLine(t, c, l + h - 1, c + a, l + h - 1, this.selectionDashArray), fabric.util.drawDashedLine(t, c, l, c, l + h, this.selectionDashArray), fabric.util.drawDashedLine(t, c + a - 1, l, c + a - 1, l + h, this.selectionDashArray), t.closePath(), t.stroke();
        }
        else
            fabric.Object.prototype._setLineDash.call(this, t, this.selectionDashArray), t.strokeRect(e.ex + o - (i > 0 ? 0 : a), e.ey + o - (r > 0 ? 0 : h), a, h); }, findTarget: function (t, e) { if (!this.skipTargetFind) {
        var i, r = !0, n = this.getPointer(t, r), s = this.getActiveGroup(), o = this.getActiveObject();
        if (s && !e && this._checkTarget(n, s))
            return this._fireOverOutEvents(s, t), s;
        if (o && o._findTargetCorner(n))
            return this._fireOverOutEvents(o, t), o;
        if (o && this._checkTarget(n, o)) {
            if (!this.preserveObjectStacking)
                return this._fireOverOutEvents(o, t), o;
            i = o;
        }
        this.targets = [];
        var a = this._searchPossibleTargets(this._objects, n);
        return t[this.altSelectionKey] && a && i && a !== i && (a = i), this._fireOverOutEvents(a, t), a;
    } }, _fireOverOutEvents: function (t, e) { t ? this._hoveredTarget !== t && (this._hoveredTarget && (this.fire("mouse:out", { target: this._hoveredTarget, e: e }), this._hoveredTarget.fire("mouseout")), this.fire("mouse:over", { target: t, e: e }), t.fire("mouseover"), this._hoveredTarget = t) : this._hoveredTarget && (this.fire("mouse:out", { target: this._hoveredTarget, e: e }), this._hoveredTarget.fire("mouseout"), this._hoveredTarget = null); }, _checkTarget: function (t, e) { if (e && e.visible && e.evented && this.containsPoint(null, e, t)) {
        if (!this.perPixelTargetFind && !e.perPixelTargetFind || e.isEditing)
            return !0;
        var i = this.isTargetTransparent(e, t.x, t.y);
        if (!i)
            return !0;
    } }, _searchPossibleTargets: function (t, e) { for (var i, r, n, s = t.length; s--;)
        if (this._checkTarget(e, t[s])) {
            i = t[s], "group" === i.type && i.subTargetCheck && (r = this._normalizePointer(i, e), n = this._searchPossibleTargets(i._objects, r), n && this.targets.push(n));
            break;
        } return i; }, restorePointerVpt: function (t) { return fabric.util.transformPoint(t, fabric.util.invertTransform(this.viewportTransform)); }, getPointer: function (e, i, r) { r || (r = this.upperCanvasEl); var n, s = t(e), o = r.getBoundingClientRect(), a = o.width || 0, h = o.height || 0; return a && h || ("top" in o && "bottom" in o && (h = Math.abs(o.top - o.bottom)), "right" in o && "left" in o && (a = Math.abs(o.right - o.left))), this.calcOffset(), s.x = s.x - this._offset.left, s.y = s.y - this._offset.top, i || (s = this.restorePointerVpt(s)), n = 0 === a || 0 === h ? { width: 1, height: 1 } : { width: r.width / a, height: r.height / h }, { x: s.x * n.width, y: s.y * n.height }; }, _createUpperCanvas: function () { var t = this.lowerCanvasEl.className.replace(/\s*lower-canvas\s*/, ""); this.upperCanvasEl = this._createCanvasElement(), fabric.util.addClass(this.upperCanvasEl, "upper-canvas " + t), this.wrapperEl.appendChild(this.upperCanvasEl), this._copyCanvasStyle(this.lowerCanvasEl, this.upperCanvasEl), this._applyCanvasStyle(this.upperCanvasEl), this.contextTop = this.upperCanvasEl.getContext("2d"); }, _createCacheCanvas: function () { this.cacheCanvasEl = this._createCanvasElement(), this.cacheCanvasEl.setAttribute("width", this.width), this.cacheCanvasEl.setAttribute("height", this.height), this.contextCache = this.cacheCanvasEl.getContext("2d"); }, _initWrapperElement: function () { this.wrapperEl = fabric.util.wrapElement(this.lowerCanvasEl, "div", { class: this.containerClass }), fabric.util.setStyle(this.wrapperEl, { width: this.getWidth() + "px", height: this.getHeight() + "px", position: "relative" }), fabric.util.makeElementUnselectable(this.wrapperEl); }, _applyCanvasStyle: function (t) { var e = this.getWidth() || t.width, i = this.getHeight() || t.height; fabric.util.setStyle(t, { position: "absolute", width: e + "px", height: i + "px", left: 0, top: 0 }), t.width = e, t.height = i, fabric.util.makeElementUnselectable(t); }, _copyCanvasStyle: function (t, e) { e.style.cssText = t.style.cssText; }, getSelectionContext: function () { return this.contextTop; }, getSelectionElement: function () { return this.upperCanvasEl; }, _setActiveObject: function (t) { this._activeObject && this._activeObject.set("active", !1), this._activeObject = t, t.set("active", !0); }, setActiveObject: function (t, e) { return this._setActiveObject(t), this.renderAll(), this.fire("object:selected", { target: t, e: e }), t.fire("selected", { e: e }), this; }, getActiveObject: function () { return this._activeObject; }, _onObjectRemoved: function (t) { this.getActiveObject() === t && (this.fire("before:selection:cleared", { target: t }), this._discardActiveObject(), this.fire("selection:cleared", { target: t }), t.fire("deselected")), this.callSuper("_onObjectRemoved", t); }, _discardActiveObject: function () { this._activeObject && this._activeObject.set("active", !1), this._activeObject = null; }, discardActiveObject: function (t) { var e = this._activeObject; return this.fire("before:selection:cleared", { target: e, e: t }), this._discardActiveObject(), this.fire("selection:cleared", { e: t }), e && e.fire("deselected", { e: t }), this; }, _setActiveGroup: function (t) { this._activeGroup = t, t && t.set("active", !0); }, setActiveGroup: function (t, e) { return this._setActiveGroup(t), t && (this.fire("object:selected", { target: t, e: e }), t.fire("selected", { e: e })), this; }, getActiveGroup: function () { return this._activeGroup; }, _discardActiveGroup: function () { var t = this.getActiveGroup(); t && t.destroy(), this.setActiveGroup(null); }, discardActiveGroup: function (t) { var e = this.getActiveGroup(); return this.fire("before:selection:cleared", { e: t, target: e }), this._discardActiveGroup(), this.fire("selection:cleared", { e: t }), this; }, deactivateAll: function () { for (var t = this.getObjects(), e = 0, i = t.length; e < i; e++)
        t[e].set("active", !1); return this._discardActiveGroup(), this._discardActiveObject(), this; }, deactivateAllWithDispatch: function (t) { var e = this.getActiveGroup(), i = this.getActiveObject(); return (i || e) && this.fire("before:selection:cleared", { target: i || e, e: t }), this.deactivateAll(), (i || e) && (this.fire("selection:cleared", { e: t, target: i }), i && i.fire("deselected")), this; }, dispose: function () { this.callSuper("dispose"); var t = this.wrapperEl; return this.removeListeners(), t.removeChild(this.upperCanvasEl), t.removeChild(this.lowerCanvasEl), delete this.upperCanvasEl, t.parentNode && t.parentNode.replaceChild(this.lowerCanvasEl, this.wrapperEl), delete this.wrapperEl, this; }, clear: function () { return this.discardActiveGroup(), this.discardActiveObject(), this.clearContext(this.contextTop), this.callSuper("clear"); }, drawControls: function (t) { var e = this.getActiveGroup(); e ? e._renderControls(t) : this._drawObjectsControls(t); }, _drawObjectsControls: function (t) { for (var e = 0, i = this._objects.length; e < i; ++e)
        this._objects[e] && this._objects[e].active && this._objects[e]._renderControls(t); }, _toObject: function (t, e, i) { var r = this._realizeGroupTransformOnObject(t), n = this.callSuper("_toObject", t, e, i); return this._unwindGroupTransformOnObject(t, r), n; }, _realizeGroupTransformOnObject: function (t) { var e = ["angle", "flipX", "flipY", "height", "left", "scaleX", "scaleY", "top", "width"]; if (t.group && t.group === this.getActiveGroup()) {
        var i = {};
        return e.forEach(function (e) { i[e] = t[e]; }), this.getActiveGroup().realizeTransform(t), i;
    } return null; }, _unwindGroupTransformOnObject: function (t, e) { e && t.set(e); }, _setSVGObject: function (t, e, i) { var r; r = this._realizeGroupTransformOnObject(e), this.callSuper("_setSVGObject", t, e, i), this._unwindGroupTransformOnObject(e, r); } }); for (var a in fabric.StaticCanvas)
    "prototype" !== a && (fabric.Canvas[a] = fabric.StaticCanvas[a]); fabric.isTouchSupported && (fabric.Canvas.prototype._setCursorFromEvent = function () { }), fabric.Element = fabric.Canvas; }(), function () {
    var t = { mt: 0, tr: 1, mr: 2, br: 3, mb: 4, bl: 5, ml: 6, tl: 7 }, e = fabric.util.addListener, i = fabric.util.removeListener;
    fabric.util.object.extend(fabric.Canvas.prototype, { cursorMap: ["n-resize", "ne-resize", "e-resize", "se-resize", "s-resize", "sw-resize", "w-resize", "nw-resize"], _initEventListeners: function () { this._bindEvents(), e(fabric.window, "resize", this._onResize), e(this.upperCanvasEl, "mousedown", this._onMouseDown), e(this.upperCanvasEl, "mousemove", this._onMouseMove), e(this.upperCanvasEl, "mouseout", this._onMouseOut), e(this.upperCanvasEl, "wheel", this._onMouseWheel), e(this.upperCanvasEl, "contextmenu", this._onContextMenu), e(this.upperCanvasEl, "touchstart", this._onMouseDown), e(this.upperCanvasEl, "touchmove", this._onMouseMove), "undefined" != typeof eventjs && "add" in eventjs && (eventjs.add(this.upperCanvasEl, "gesture", this._onGesture), eventjs.add(this.upperCanvasEl, "drag", this._onDrag), eventjs.add(this.upperCanvasEl, "orientation", this._onOrientationChange), eventjs.add(this.upperCanvasEl, "shake", this._onShake), eventjs.add(this.upperCanvasEl, "longpress", this._onLongPress)); }, _bindEvents: function () {
            this._onMouseDown = this._onMouseDown.bind(this), this._onMouseMove = this._onMouseMove.bind(this), this._onMouseUp = this._onMouseUp.bind(this), this._onResize = this._onResize.bind(this), this._onGesture = this._onGesture.bind(this), this._onDrag = this._onDrag.bind(this),
                this._onShake = this._onShake.bind(this), this._onLongPress = this._onLongPress.bind(this), this._onOrientationChange = this._onOrientationChange.bind(this), this._onMouseWheel = this._onMouseWheel.bind(this), this._onMouseOut = this._onMouseOut.bind(this), this._onContextMenu = this._onContextMenu.bind(this);
        }, removeListeners: function () { i(fabric.window, "resize", this._onResize), i(this.upperCanvasEl, "mousedown", this._onMouseDown), i(this.upperCanvasEl, "mousemove", this._onMouseMove), i(this.upperCanvasEl, "mouseout", this._onMouseOut), i(this.upperCanvasEl, "wheel", this._onMouseWheel), i(this.upperCanvasEl, "contextmenu", this._onContextMenu), i(this.upperCanvasEl, "touchstart", this._onMouseDown), i(this.upperCanvasEl, "touchmove", this._onMouseMove), "undefined" != typeof eventjs && "remove" in eventjs && (eventjs.remove(this.upperCanvasEl, "gesture", this._onGesture), eventjs.remove(this.upperCanvasEl, "drag", this._onDrag), eventjs.remove(this.upperCanvasEl, "orientation", this._onOrientationChange), eventjs.remove(this.upperCanvasEl, "shake", this._onShake), eventjs.remove(this.upperCanvasEl, "longpress", this._onLongPress)); }, _onGesture: function (t, e) { this.__onTransformGesture && this.__onTransformGesture(t, e); }, _onDrag: function (t, e) { this.__onDrag && this.__onDrag(t, e); }, _onMouseWheel: function (t) { this.__onMouseWheel(t); }, _onMouseOut: function (t) { var e = this._hoveredTarget; this.fire("mouse:out", { target: e, e: t }), this._hoveredTarget = null, e && e.fire("mouseout", { e: t }); }, _onOrientationChange: function (t, e) { this.__onOrientationChange && this.__onOrientationChange(t, e); }, _onShake: function (t, e) { this.__onShake && this.__onShake(t, e); }, _onLongPress: function (t, e) { this.__onLongPress && this.__onLongPress(t, e); }, _onContextMenu: function (t) { return this.stopContextMenu && (t.stopPropagation(), t.preventDefault()), !1; }, _onMouseDown: function (t) { this.__onMouseDown(t), e(fabric.document, "touchend", this._onMouseUp), e(fabric.document, "touchmove", this._onMouseMove), i(this.upperCanvasEl, "mousemove", this._onMouseMove), i(this.upperCanvasEl, "touchmove", this._onMouseMove), "touchstart" === t.type ? i(this.upperCanvasEl, "mousedown", this._onMouseDown) : (e(fabric.document, "mouseup", this._onMouseUp), e(fabric.document, "mousemove", this._onMouseMove)); }, _onMouseUp: function (t) { if (this.__onMouseUp(t), i(fabric.document, "mouseup", this._onMouseUp), i(fabric.document, "touchend", this._onMouseUp), i(fabric.document, "mousemove", this._onMouseMove), i(fabric.document, "touchmove", this._onMouseMove), e(this.upperCanvasEl, "mousemove", this._onMouseMove), e(this.upperCanvasEl, "touchmove", this._onMouseMove), "touchend" === t.type) {
            var r = this;
            setTimeout(function () { e(r.upperCanvasEl, "mousedown", r._onMouseDown); }, 400);
        } }, _onMouseMove: function (t) { !this.allowTouchScrolling && t.preventDefault && t.preventDefault(), this.__onMouseMove(t); }, _onResize: function () { this.calcOffset(); }, _shouldRender: function (t, e) { var i = this.getActiveGroup() || this.getActiveObject(); return !!(t && (t.isMoving || t !== i) || !t && i || !t && !i && !this._groupSelector || e && this._previousPointer && this.selection && (e.x !== this._previousPointer.x || e.y !== this._previousPointer.y)); }, __onMouseUp: function (t) { var e, i = !0, r = this._currentTransform, n = this._groupSelector, s = !n || 0 === n.left && 0 === n.top; if (this.isDrawingMode && this._isCurrentlyDrawing)
            return void this._onMouseUpInDrawingMode(t); r && (this._finalizeCurrentTransform(), i = !r.actionPerformed), e = i ? this.findTarget(t, !0) : r.target; var o = this._shouldRender(e, this.getPointer(t)); e || !s ? this._maybeGroupObjects(t) : (this._groupSelector = null, this._currentTransform = null), e && (e.isMoving = !1), this._handleCursorAndEvent(t, e, "up"), e && (e.__corner = 0), o && this.renderAll(); }, _handleCursorAndEvent: function (t, e, i) { this._setCursorFromEvent(t, e), this._handleEvent(t, i, e ? e : null); }, _handleEvent: function (t, e, i) { var r = void 0 === typeof i ? this.findTarget(t) : i, n = this.targets || [], s = { e: t, target: r, subTargets: n }; this.fire("mouse:" + e, s), r && r.fire("mouse" + e, s); for (var o = 0; o < n.length; o++)
            n[o].fire("mouse" + e, s); }, _finalizeCurrentTransform: function () { var t = this._currentTransform, e = t.target; e._scaling && (e._scaling = !1), e.setCoords(), this._restoreOriginXY(e), (t.actionPerformed || this.stateful && e.hasStateChanged()) && (this.fire("object:modified", { target: e }), e.fire("modified")); }, _restoreOriginXY: function (t) { if (this._previousOriginX && this._previousOriginY) {
            var e = t.translateToOriginPoint(t.getCenterPoint(), this._previousOriginX, this._previousOriginY);
            t.originX = this._previousOriginX, t.originY = this._previousOriginY, t.left = e.x, t.top = e.y, this._previousOriginX = null, this._previousOriginY = null;
        } }, _onMouseDownInDrawingMode: function (t) { this._isCurrentlyDrawing = !0, this.discardActiveObject(t).renderAll(), this.clipTo && fabric.util.clipContext(this, this.contextTop); var e = this.getPointer(t); this.freeDrawingBrush.onMouseDown(e), this._handleEvent(t, "down"); }, _onMouseMoveInDrawingMode: function (t) { if (this._isCurrentlyDrawing) {
            var e = this.getPointer(t);
            this.freeDrawingBrush.onMouseMove(e);
        } this.setCursor(this.freeDrawingCursor), this._handleEvent(t, "move"); }, _onMouseUpInDrawingMode: function (t) { this._isCurrentlyDrawing = !1, this.clipTo && this.contextTop.restore(), this.freeDrawingBrush.onMouseUp(), this._handleEvent(t, "up"); }, __onMouseDown: function (t) { var e = "which" in t ? 3 === t.which : 2 === t.button; if (e)
            return void (this.fireRightClick && this._handleEvent(t, "down", i ? i : null)); if (this.isDrawingMode)
            return void this._onMouseDownInDrawingMode(t); if (!this._currentTransform) {
            var i = this.findTarget(t), r = this.getPointer(t, !0);
            this._previousPointer = r;
            var n = this._shouldRender(i, r), s = this._shouldGroup(t, i);
            this._shouldClearSelection(t, i) ? this._clearSelection(t, i, r) : s && (this._handleGrouping(t, i), i = this.getActiveGroup()), i && (!i.selectable || !i.__corner && s || (this._beforeTransform(t, i), this._setupCurrentTransform(t, i)), i !== this.getActiveGroup() && i !== this.getActiveObject() && (this.deactivateAll(), i.selectable && this.setActiveObject(i, t))), this._handleEvent(t, "down", i ? i : null), n && this.renderAll();
        } }, _beforeTransform: function (t, e) { this.stateful && e.saveState(), e._findTargetCorner(this.getPointer(t)) && this.onBeforeScaleRotate(e); }, _clearSelection: function (t, e, i) { this.deactivateAllWithDispatch(t), e && e.selectable ? this.setActiveObject(e, t) : this.selection && (this._groupSelector = { ex: i.x, ey: i.y, top: 0, left: 0 }); }, _setOriginToCenter: function (t) { this._previousOriginX = this._currentTransform.target.originX, this._previousOriginY = this._currentTransform.target.originY; var e = t.getCenterPoint(); t.originX = "center", t.originY = "center", t.left = e.x, t.top = e.y, this._currentTransform.left = t.left, this._currentTransform.top = t.top; }, _setCenterToOrigin: function (t) { var e = t.translateToOriginPoint(t.getCenterPoint(), this._previousOriginX, this._previousOriginY); t.originX = this._previousOriginX, t.originY = this._previousOriginY, t.left = e.x, t.top = e.y, this._previousOriginX = null, this._previousOriginY = null; }, __onMouseMove: function (t) { var e, i; if (this.isDrawingMode)
            return void this._onMouseMoveInDrawingMode(t); if (!("undefined" != typeof t.touches && t.touches.length > 1)) {
            var r = this._groupSelector;
            r ? (i = this.getPointer(t, !0), r.left = i.x - r.ex, r.top = i.y - r.ey, this.renderTop()) : this._currentTransform ? this._transformObject(t) : (e = this.findTarget(t), this._setCursorFromEvent(t, e)), this._handleEvent(t, "move", e ? e : null);
        } }, __onMouseWheel: function (t) { this.fire("mouse:wheel", { e: t }); }, _transformObject: function (t) { var e = this.getPointer(t), i = this._currentTransform; i.reset = !1, i.target.isMoving = !0, this._beforeScaleTransform(t, i), this._performTransformAction(t, i, e), i.actionPerformed && this.renderAll(); }, _performTransformAction: function (t, e, i) { var r = i.x, n = i.y, s = e.target, o = e.action, a = !1; "rotate" === o ? (a = this._rotateObject(r, n)) && this._fire("rotating", s, t) : "scale" === o ? (a = this._onScale(t, e, r, n)) && this._fire("scaling", s, t) : "scaleX" === o ? (a = this._scaleObject(r, n, "x")) && this._fire("scaling", s, t) : "scaleY" === o ? (a = this._scaleObject(r, n, "y")) && this._fire("scaling", s, t) : "skewX" === o ? (a = this._skewObject(r, n, "x")) && this._fire("skewing", s, t) : "skewY" === o ? (a = this._skewObject(r, n, "y")) && this._fire("skewing", s, t) : (a = this._translateObject(r, n), a && (this._fire("moving", s, t), this.setCursor(s.moveCursor || this.moveCursor))), e.actionPerformed = a; }, _fire: function (t, e, i) { this.fire("object:" + t, { target: e, e: i }), e.fire(t, { e: i }); }, _beforeScaleTransform: function (t, e) { if ("scale" === e.action || "scaleX" === e.action || "scaleY" === e.action) {
            var i = this._shouldCenterTransform(e.target);
            (i && ("center" !== e.originX || "center" !== e.originY) || !i && "center" === e.originX && "center" === e.originY) && (this._resetCurrentTransform(), e.reset = !0);
        } }, _onScale: function (t, e, i, r) { return !t[this.uniScaleKey] && !this.uniScaleTransform || e.target.get("lockUniScaling") ? (e.reset || "scale" !== e.currentAction || this._resetCurrentTransform(), e.currentAction = "scaleEqually", this._scaleObject(i, r, "equally")) : (e.currentAction = "scale", this._scaleObject(i, r)); }, _setCursorFromEvent: function (t, e) { if (!e)
            return this.setCursor(this.defaultCursor), !1; var i = e.hoverCursor || this.hoverCursor; if (e.selectable) {
            var r = this.getActiveGroup(), n = e._findTargetCorner && (!r || !r.contains(e)) && e._findTargetCorner(this.getPointer(t, !0));
            n ? this._setCornerCursor(n, e, t) : this.setCursor(i);
        }
        else
            this.setCursor(i); return !0; }, _setCornerCursor: function (e, i, r) { if (e in t)
            this.setCursor(this._getRotatedCornerCursor(e, i, r));
        else {
            if ("mtr" !== e || !i.hasRotatingPoint)
                return this.setCursor(this.defaultCursor), !1;
            this.setCursor(this.rotationCursor);
        } }, _getRotatedCornerCursor: function (e, i, r) { var n = Math.round(i.getAngle() % 360 / 45); return n < 0 && (n += 8), n += t[e], r[this.altActionKey] && t[e] % 2 === 0 && (n += 2), n %= 8, this.cursorMap[n]; } });
}(), function () { var t = Math.min, e = Math.max; fabric.util.object.extend(fabric.Canvas.prototype, { _shouldGroup: function (t, e) { var i = this.getActiveObject(); return t[this.selectionKey] && e && e.selectable && (this.getActiveGroup() || i && i !== e) && this.selection; }, _handleGrouping: function (t, e) { var i = this.getActiveGroup(); (e !== i || (e = this.findTarget(t, !0))) && (i ? this._updateActiveGroup(e, t) : this._createActiveGroup(e, t), this._activeGroup && this._activeGroup.saveCoords()); }, _updateActiveGroup: function (t, e) { var i = this.getActiveGroup(); if (i.contains(t)) {
        if (i.removeWithUpdate(t), t.set("active", !1), 1 === i.size())
            return this.discardActiveGroup(e), void this.setActiveObject(i.item(0));
    }
    else
        i.addWithUpdate(t); this.fire("selection:created", { target: i, e: e }), i.set("active", !0); }, _createActiveGroup: function (t, e) { if (this._activeObject && t !== this._activeObject) {
        var i = this._createGroup(t);
        i.addWithUpdate(), this.setActiveGroup(i), this._activeObject = null, this.fire("selection:created", { target: i, e: e });
    } t.set("active", !0); }, _createGroup: function (t) { var e = this.getObjects(), i = e.indexOf(this._activeObject) < e.indexOf(t), r = i ? [this._activeObject, t] : [t, this._activeObject]; return this._activeObject.isEditing && this._activeObject.exitEditing(), new fabric.Group(r, { canvas: this }); }, _groupSelectedObjects: function (t) { var e = this._collectObjects(); 1 === e.length ? this.setActiveObject(e[0], t) : e.length > 1 && (e = new fabric.Group(e.reverse(), { canvas: this }), e.addWithUpdate(), this.setActiveGroup(e, t), e.saveCoords(), this.fire("selection:created", { target: e }), this.renderAll()); }, _collectObjects: function () { for (var i, r = [], n = this._groupSelector.ex, s = this._groupSelector.ey, o = n + this._groupSelector.left, a = s + this._groupSelector.top, h = new fabric.Point(t(n, o), t(s, a)), c = new fabric.Point(e(n, o), e(s, a)), l = n === o && s === a, u = this._objects.length; u-- && (i = this._objects[u], !(i && i.selectable && i.visible && (i.intersectsWithRect(h, c) || i.isContainedWithinRect(h, c) || i.containsPoint(h) || i.containsPoint(c)) && (i.set("active", !0), r.push(i), l)));)
        ; return r; }, _maybeGroupObjects: function (t) { this.selection && this._groupSelector && this._groupSelectedObjects(t); var e = this.getActiveGroup(); e && (e.setObjectsCoords().setCoords(), e.isMoving = !1, this.setCursor(this.defaultCursor)), this._groupSelector = null, this._currentTransform = null; } }); }(), function () { var t = fabric.StaticCanvas.supports("toDataURLWithQuality"); fabric.util.object.extend(fabric.StaticCanvas.prototype, { toDataURL: function (t) { t || (t = {}); var e = t.format || "png", i = t.quality || 1, r = t.multiplier || 1, n = { left: t.left || 0, top: t.top || 0, width: t.width || 0, height: t.height || 0 }; return this.__toDataURLWithMultiplier(e, i, n, r); }, __toDataURLWithMultiplier: function (t, e, i, r) { var n = this.getWidth(), s = this.getHeight(), o = (i.width || this.getWidth()) * r, a = (i.height || this.getHeight()) * r, h = this.getZoom(), c = h * r, l = this.viewportTransform, u = (l[4] - i.left) * r, f = (l[5] - i.top) * r, d = [c, 0, 0, c, u, f], g = this.interactive; this.viewportTransform = d, this.interactive && (this.interactive = !1), n !== o || s !== a ? this.setDimensions({ width: o, height: a }) : this.renderAll(); var p = this.__toDataURL(t, e, i); return g && (this.interactive = g), this.viewportTransform = l, this.setDimensions({ width: n, height: s }), p; }, __toDataURL: function (e, i) { var r = this.contextContainer.canvas; "jpg" === e && (e = "jpeg"); var n = t ? r.toDataURL("image/" + e, i) : r.toDataURL("image/" + e); return n; }, toDataURLWithMultiplier: function (t, e, i) { return this.toDataURL({ format: t, multiplier: e, quality: i }); } }); }(), fabric.util.object.extend(fabric.StaticCanvas.prototype, { loadFromDatalessJSON: function (t, e, i) { return this.loadFromJSON(t, e, i); }, loadFromJSON: function (t, e, i) { if (t) {
        var r = "string" == typeof t ? JSON.parse(t) : fabric.util.object.clone(t);
        this.clear();
        var n = this;
        return this._enlivenObjects(r.objects, function () { n._setBgOverlay(r, function () { delete r.objects, delete r.backgroundImage, delete r.overlayImage, delete r.background, delete r.overlay; for (var t in r)
            n[t] = r[t]; e && e(); }); }, i), this;
    } }, _setBgOverlay: function (t, e) { var i = this, r = { backgroundColor: !1, overlayColor: !1, backgroundImage: !1, overlayImage: !1 }; if (!(t.backgroundImage || t.overlayImage || t.background || t.overlay))
        return void (e && e()); var n = function () { r.backgroundImage && r.overlayImage && r.backgroundColor && r.overlayColor && (i.renderAll(), e && e()); }; this.__setBgOverlay("backgroundImage", t.backgroundImage, r, n), this.__setBgOverlay("overlayImage", t.overlayImage, r, n), this.__setBgOverlay("backgroundColor", t.background, r, n), this.__setBgOverlay("overlayColor", t.overlay, r, n), n(); }, __setBgOverlay: function (t, e, i, r) { var n = this; return e ? void ("backgroundImage" === t || "overlayImage" === t ? fabric.Image.fromObject(e, function (e) { n[t] = e, i[t] = !0, r && r(); }) : this["set" + fabric.util.string.capitalize(t, !0)](e, function () { i[t] = !0, r && r(); })) : void (i[t] = !0); }, _enlivenObjects: function (t, e, i) { var r = this; if (!t || 0 === t.length)
        return void (e && e()); var n = this.renderOnAddRemove; this.renderOnAddRemove = !1, fabric.util.enlivenObjects(t, function (t) { t.forEach(function (t, e) { r.insertAt(t, e); }), r.renderOnAddRemove = n, e && e(); }, null, i); }, _toDataURL: function (t, e) { this.clone(function (i) { e(i.toDataURL(t)); }); }, _toDataURLWithMultiplier: function (t, e, i) { this.clone(function (r) { i(r.toDataURLWithMultiplier(t, e)); }); }, clone: function (t, e) { var i = JSON.stringify(this.toJSON(e)); this.cloneWithoutData(function (e) { e.loadFromJSON(i, function () { t && t(e); }); }); }, cloneWithoutData: function (t) { var e = fabric.document.createElement("canvas"); e.width = this.getWidth(), e.height = this.getHeight(); var i = new fabric.Canvas(e); i.clipTo = this.clipTo, this.backgroundImage ? (i.setBackgroundImage(this.backgroundImage.src, function () { i.renderAll(), t && t(i); }), i.backgroundImageOpacity = this.backgroundImageOpacity, i.backgroundImageStretch = this.backgroundImageStretch) : t && t(i); } }), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = e.util.object.extend, r = e.util.toFixed, n = e.util.string.capitalize, s = e.util.degreesToRadians, o = e.StaticCanvas.supports("setLineDash");
    e.Object || (e.Object = e.util.createClass({ type: "object", originX: "left", originY: "top", top: 0, left: 0, width: 0, height: 0, scaleX: 1, scaleY: 1, flipX: !1, flipY: !1, opacity: 1, angle: 0, skewX: 0, skewY: 0, cornerSize: 13, transparentCorners: !0, hoverCursor: null, moveCursor: null, padding: 0, borderColor: "rgba(102,153,255,0.75)", borderDashArray: null, cornerColor: "rgba(102,153,255,0.5)", cornerStrokeColor: null, cornerStyle: "rect", cornerDashArray: null, centeredScaling: !1, centeredRotation: !0, fill: "rgb(0,0,0)", fillRule: "nonzero", globalCompositeOperation: "source-over", backgroundColor: "", selectionBackgroundColor: "", stroke: null, strokeWidth: 1, strokeDashArray: null, strokeLineCap: "butt", strokeLineJoin: "miter", strokeMiterLimit: 10, shadow: null, borderOpacityWhenMoving: .4, borderScaleFactor: 1, transformMatrix: null, minScaleLimit: .01, selectable: !0, evented: !0, visible: !0, hasControls: !0, hasBorders: !0, hasRotatingPoint: !0, rotatingPointOffset: 40, perPixelTargetFind: !1, includeDefaultValues: !0, clipTo: null, lockMovementX: !1, lockMovementY: !1, lockRotation: !1, lockScalingX: !1, lockScalingY: !1, lockUniScaling: !1, lockSkewingX: !1, lockSkewingY: !1, lockScalingFlip: !1, excludeFromExport: !1, stateProperties: "top left width height scaleX scaleY flipX flipY originX originY transformMatrix stroke strokeWidth strokeDashArray strokeLineCap strokeLineJoin strokeMiterLimit angle opacity fill fillRule globalCompositeOperation shadow clipTo visible backgroundColor skewX skewY".split(" "), initialize: function (t) { t && this.setOptions(t); }, _initGradient: function (t) { !t.fill || !t.fill.colorStops || t.fill instanceof e.Gradient || this.set("fill", new e.Gradient(t.fill)), !t.stroke || !t.stroke.colorStops || t.stroke instanceof e.Gradient || this.set("stroke", new e.Gradient(t.stroke)); }, _initPattern: function (t) { !t.fill || !t.fill.source || t.fill instanceof e.Pattern || this.set("fill", new e.Pattern(t.fill)), !t.stroke || !t.stroke.source || t.stroke instanceof e.Pattern || this.set("stroke", new e.Pattern(t.stroke)); }, _initClipping: function (t) { if (t.clipTo && "string" == typeof t.clipTo) {
            var i = e.util.getFunctionBody(t.clipTo);
            "undefined" != typeof i && (this.clipTo = new Function("ctx", i));
        } }, setOptions: function (t) { for (var e in t)
            this.set(e, t[e]); this._initGradient(t), this._initPattern(t), this._initClipping(t); }, transform: function (t, e) { this.group && !this.group._transformDone && this.group === this.canvas._activeGroup && this.group.transform(t); var i = e ? this._getLeftTopCoords() : this.getCenterPoint(); t.translate(i.x, i.y), t.rotate(s(this.angle)), t.scale(this.scaleX * (this.flipX ? -1 : 1), this.scaleY * (this.flipY ? -1 : 1)), t.transform(1, 0, Math.tan(s(this.skewX)), 1, 0, 0), t.transform(1, Math.tan(s(this.skewY)), 0, 1, 0, 0); }, toObject: function (t) { var i = e.Object.NUM_FRACTION_DIGITS, n = { type: this.type, originX: this.originX, originY: this.originY, left: r(this.left, i), top: r(this.top, i), width: r(this.width, i), height: r(this.height, i), fill: this.fill && this.fill.toObject ? this.fill.toObject() : this.fill, stroke: this.stroke && this.stroke.toObject ? this.stroke.toObject() : this.stroke, strokeWidth: r(this.strokeWidth, i), strokeDashArray: this.strokeDashArray ? this.strokeDashArray.concat() : this.strokeDashArray, strokeLineCap: this.strokeLineCap, strokeLineJoin: this.strokeLineJoin, strokeMiterLimit: r(this.strokeMiterLimit, i), scaleX: r(this.scaleX, i), scaleY: r(this.scaleY, i), angle: r(this.getAngle(), i), flipX: this.flipX, flipY: this.flipY, opacity: r(this.opacity, i), shadow: this.shadow && this.shadow.toObject ? this.shadow.toObject() : this.shadow, visible: this.visible, clipTo: this.clipTo && String(this.clipTo), backgroundColor: this.backgroundColor, fillRule: this.fillRule, globalCompositeOperation: this.globalCompositeOperation, transformMatrix: this.transformMatrix ? this.transformMatrix.concat() : this.transformMatrix, skewX: r(this.skewX, i), skewY: r(this.skewY, i) }; return this.includeDefaultValues || (n = this._removeDefaultValues(n)), e.util.populateWithProperties(this, n, t), n; }, toDatalessObject: function (t) { return this.toObject(t); }, _removeDefaultValues: function (t) { var i = e.util.getKlass(t.type).prototype, r = i.stateProperties; return r.forEach(function (e) { t[e] === i[e] && delete t[e]; var r = "[object Array]" === Object.prototype.toString.call(t[e]) && "[object Array]" === Object.prototype.toString.call(i[e]); r && 0 === t[e].length && 0 === i[e].length && delete t[e]; }), t; }, toString: function () { return "#<fabric." + n(this.type) + ">"; }, get: function (t) { return this[t]; }, getObjectScaling: function () { var t = this.scaleX, e = this.scaleY; if (this.group) {
            var i = this.group.getObjectScaling();
            t *= i.scaleX, e *= i.scaleY;
        } return { scaleX: t, scaleY: e }; }, _setObject: function (t) { for (var e in t)
            this._set(e, t[e]); }, set: function (t, e) { return "object" == typeof t ? this._setObject(t) : "function" == typeof e && "clipTo" !== t ? this._set(t, e(this.get(t))) : this._set(t, e), this; }, _set: function (t, i) { var r = "scaleX" === t || "scaleY" === t; return r && (i = this._constrainScale(i)), "scaleX" === t && i < 0 ? (this.flipX = !this.flipX, i *= -1) : "scaleY" === t && i < 0 ? (this.flipY = !this.flipY, i *= -1) : "shadow" !== t || !i || i instanceof e.Shadow || (i = new e.Shadow(i)), this[t] = i, "width" !== t && "height" !== t || (this.minScaleLimit = Math.min(.1, 1 / Math.max(this.width, this.height))), this; }, setOnGroup: function () { }, toggle: function (t) { var e = this.get(t); return "boolean" == typeof e && this.set(t, !e), this; }, setSourcePath: function (t) { return this.sourcePath = t, this; }, getViewportTransform: function () { return this.canvas && this.canvas.viewportTransform ? this.canvas.viewportTransform : [1, 0, 0, 1, 0, 0]; }, render: function (t, i) { 0 === this.width && 0 === this.height || !this.visible || (t.save(), this._setupCompositeOperation(t), this.drawSelectionBackground(t), i || this.transform(t), this._setOpacity(t), this._setShadow(t), this._renderBackground(t), this._setStrokeStyles(t), this._setFillStyles(t), this.transformMatrix && t.transform.apply(t, this.transformMatrix), this.clipTo && e.util.clipContext(this, t), this._render(t, i), this.clipTo && t.restore(), t.restore()); }, _renderBackground: function (t) { this.backgroundColor && (t.fillStyle = this.backgroundColor, t.fillRect(-this.width / 2, -this.height / 2, this.width, this.height), this._removeShadow(t)); }, _setOpacity: function (t) { this.group && this.group._setOpacity(t), t.globalAlpha *= this.opacity; }, _setStrokeStyles: function (t) { this.stroke && (t.lineWidth = this.strokeWidth, t.lineCap = this.strokeLineCap, t.lineJoin = this.strokeLineJoin, t.miterLimit = this.strokeMiterLimit, t.strokeStyle = this.stroke.toLive ? this.stroke.toLive(t, this) : this.stroke); }, _setFillStyles: function (t) { this.fill && (t.fillStyle = this.fill.toLive ? this.fill.toLive(t, this) : this.fill); }, _setLineDash: function (t, e, i) { e && (1 & e.length && e.push.apply(e, e), o ? t.setLineDash(e) : i && i(t)); }, _renderControls: function (t, i) { if (!(!this.active || i || this.group && this.group !== this.canvas.getActiveGroup())) {
            var r, n = this.getViewportTransform(), o = this.calcTransformMatrix();
            o = e.util.multiplyTransformMatrices(n, o), r = e.util.qrDecompose(o), t.save(), t.translate(r.translateX, r.translateY), t.lineWidth = 1 * this.borderScaleFactor, t.globalAlpha = this.isMoving ? this.borderOpacityWhenMoving : 1, this.group && this.group === this.canvas.getActiveGroup() ? (t.rotate(s(r.angle)), this.drawBordersInGroup(t, r)) : (t.rotate(s(this.angle)), this.drawBorders(t)), this.drawControls(t), t.restore();
        } }, _setShadow: function (t) { if (this.shadow) {
            var i = this.canvas && this.canvas.viewportTransform[0] || 1, r = this.canvas && this.canvas.viewportTransform[3] || 1, n = this.getObjectScaling();
            this.canvas && this.canvas._isRetinaScaling() && (i *= e.devicePixelRatio, r *= e.devicePixelRatio), t.shadowColor = this.shadow.color, t.shadowBlur = this.shadow.blur * (i + r) * (n.scaleX + n.scaleY) / 4, t.shadowOffsetX = this.shadow.offsetX * i * n.scaleX, t.shadowOffsetY = this.shadow.offsetY * r * n.scaleY;
        } }, _removeShadow: function (t) { this.shadow && (t.shadowColor = "", t.shadowBlur = t.shadowOffsetX = t.shadowOffsetY = 0); }, _renderFill: function (t) { if (this.fill) {
            if (t.save(), this.fill.gradientTransform) {
                var e = this.fill.gradientTransform;
                t.transform.apply(t, e);
            }
            this.fill.toLive && t.translate(-this.width / 2 + this.fill.offsetX || 0, -this.height / 2 + this.fill.offsetY || 0), "evenodd" === this.fillRule ? t.fill("evenodd") : t.fill(), t.restore();
        } }, _renderStroke: function (t) { if (this.stroke && 0 !== this.strokeWidth) {
            if (this.shadow && !this.shadow.affectStroke && this._removeShadow(t), t.save(), this._setLineDash(t, this.strokeDashArray, this._renderDashedStroke), this.stroke.gradientTransform) {
                var e = this.stroke.gradientTransform;
                t.transform.apply(t, e);
            }
            this.stroke.toLive && t.translate(-this.width / 2 + this.stroke.offsetX || 0, -this.height / 2 + this.stroke.offsetY || 0), t.stroke(), t.restore();
        } }, clone: function (t, i) { return this.constructor.fromObject ? this.constructor.fromObject(this.toObject(i), t) : new e.Object(this.toObject(i)); }, cloneAsImage: function (t, i) { var r = this.toDataURL(i); return e.util.loadImage(r, function (i) { t && t(new e.Image(i)); }), this; }, toDataURL: function (t) { t || (t = {}); var i = e.util.createCanvasElement(), r = this.getBoundingRect(); i.width = r.width, i.height = r.height, e.util.wrapElement(i, "div"); var n = new e.StaticCanvas(i, { enableRetinaScaling: t.enableRetinaScaling }); "jpg" === t.format && (t.format = "jpeg"), "jpeg" === t.format && (n.backgroundColor = "#fff"); var s = { active: this.get("active"), left: this.getLeft(), top: this.getTop() }; this.set("active", !1), this.setPositionByOrigin(new e.Point(n.getWidth() / 2, n.getHeight() / 2), "center", "center"); var o = this.canvas; n.add(this); var a = n.toDataURL(t); return this.set(s).setCoords(), this.canvas = o, n.dispose(), n = null, a; }, isType: function (t) { return this.type === t; }, complexity: function () { return 0; }, toJSON: function (t) { return this.toObject(t); }, setGradient: function (t, i) { i || (i = {}); var r = { colorStops: [] }; r.type = i.type || (i.r1 || i.r2 ? "radial" : "linear"), r.coords = { x1: i.x1, y1: i.y1, x2: i.x2, y2: i.y2 }, (i.r1 || i.r2) && (r.coords.r1 = i.r1, r.coords.r2 = i.r2), i.gradientTransform && (r.gradientTransform = i.gradientTransform); for (var n in i.colorStops) {
            var s = new e.Color(i.colorStops[n]);
            r.colorStops.push({ offset: n, color: s.toRgb(), opacity: s.getAlpha() });
        } return this.set(t, e.Gradient.forObject(this, r)); }, setPatternFill: function (t) { return this.set("fill", new e.Pattern(t)); }, setShadow: function (t) { return this.set("shadow", t ? new e.Shadow(t) : null); }, setColor: function (t) { return this.set("fill", t), this; }, setAngle: function (t) { var e = ("center" !== this.originX || "center" !== this.originY) && this.centeredRotation; return e && this._setOriginToCenter(), this.set("angle", t), e && this._resetOrigin(), this; }, centerH: function () { return this.canvas && this.canvas.centerObjectH(this), this; }, viewportCenterH: function () { return this.canvas && this.canvas.viewportCenterObjectH(this), this; }, centerV: function () { return this.canvas && this.canvas.centerObjectV(this), this; }, viewportCenterV: function () { return this.canvas && this.canvas.viewportCenterObjectV(this), this; }, center: function () { return this.canvas && this.canvas.centerObject(this), this; }, viewportCenter: function () { return this.canvas && this.canvas.viewportCenterObject(this), this; }, remove: function () { return this.canvas && this.canvas.remove(this), this; }, getLocalPointer: function (t, i) { i = i || this.canvas.getPointer(t); var r = new e.Point(i.x, i.y), n = this._getLeftTopCoords(); return this.angle && (r = e.util.rotatePoint(r, n, e.util.degreesToRadians(-this.angle))), { x: r.x - n.x, y: r.y - n.y }; }, _setupCompositeOperation: function (t) { this.globalCompositeOperation && (t.globalCompositeOperation = this.globalCompositeOperation); } }), e.util.createAccessors(e.Object), e.Object.prototype.rotate = e.Object.prototype.setAngle, i(e.Object.prototype, e.Observable), e.Object.NUM_FRACTION_DIGITS = 2, e.Object.__uid = 0);
}("undefined" != typeof exports ? exports : this), function () { var t = fabric.util.degreesToRadians, e = { left: -.5, center: 0, right: .5 }, i = { top: -.5, center: 0, bottom: .5 }; fabric.util.object.extend(fabric.Object.prototype, { translateToGivenOrigin: function (t, r, n, s, o) { var a, h, c, l = t.x, u = t.y; return "string" == typeof r ? r = e[r] : r -= .5, "string" == typeof s ? s = e[s] : s -= .5, a = s - r, "string" == typeof n ? n = i[n] : n -= .5, "string" == typeof o ? o = i[o] : o -= .5, h = o - n, (a || h) && (c = this._getTransformedDimensions(), l = t.x + a * c.x, u = t.y + h * c.y), new fabric.Point(l, u); }, translateToCenterPoint: function (e, i, r) { var n = this.translateToGivenOrigin(e, i, r, "center", "center"); return this.angle ? fabric.util.rotatePoint(n, e, t(this.angle)) : n; }, translateToOriginPoint: function (e, i, r) { var n = this.translateToGivenOrigin(e, "center", "center", i, r); return this.angle ? fabric.util.rotatePoint(n, e, t(this.angle)) : n; }, getCenterPoint: function () { var t = new fabric.Point(this.left, this.top); return this.translateToCenterPoint(t, this.originX, this.originY); }, getPointByOrigin: function (t, e) { var i = this.getCenterPoint(); return this.translateToOriginPoint(i, t, e); }, toLocalPoint: function (e, i, r) { var n, s, o = this.getCenterPoint(); return n = "undefined" != typeof i && "undefined" != typeof r ? this.translateToGivenOrigin(o, "center", "center", i, r) : new fabric.Point(this.left, this.top), s = new fabric.Point(e.x, e.y), this.angle && (s = fabric.util.rotatePoint(s, o, -t(this.angle))), s.subtractEquals(n); }, setPositionByOrigin: function (t, e, i) { var r = this.translateToCenterPoint(t, e, i), n = this.translateToOriginPoint(r, this.originX, this.originY); this.set("left", n.x), this.set("top", n.y); }, adjustPosition: function (i) { var r, n, s = t(this.angle), o = this.getWidth(), a = Math.cos(s) * o, h = Math.sin(s) * o; r = "string" == typeof this.originX ? e[this.originX] : this.originX - .5, n = "string" == typeof i ? e[i] : i - .5, this.left += a * (n - r), this.top += h * (n - r), this.setCoords(), this.originX = i; }, _setOriginToCenter: function () { this._originalOriginX = this.originX, this._originalOriginY = this.originY; var t = this.getCenterPoint(); this.originX = "center", this.originY = "center", this.left = t.x, this.top = t.y; }, _resetOrigin: function () { var t = this.translateToOriginPoint(this.getCenterPoint(), this._originalOriginX, this._originalOriginY); this.originX = this._originalOriginX, this.originY = this._originalOriginY, this.left = t.x, this.top = t.y, this._originalOriginX = null, this._originalOriginY = null; }, _getLeftTopCoords: function () { return this.translateToOriginPoint(this.getCenterPoint(), "left", "top"); } }); }(), function () {
    function t(t) { return [new fabric.Point(t.tl.x, t.tl.y), new fabric.Point(t.tr.x, t.tr.y), new fabric.Point(t.br.x, t.br.y), new fabric.Point(t.bl.x, t.bl.y)]; }
    var e = fabric.util.degreesToRadians, i = fabric.util.multiplyTransformMatrices;
    fabric.util.object.extend(fabric.Object.prototype, { oCoords: null, intersectsWithRect: function (e, i) { var r = t(this.oCoords), n = fabric.Intersection.intersectPolygonRectangle(r, e, i); return "Intersection" === n.status; }, intersectsWithObject: function (e) { var i = fabric.Intersection.intersectPolygonPolygon(t(this.oCoords), t(e.oCoords)); return "Intersection" === i.status || e.isContainedWithinObject(this) || this.isContainedWithinObject(e); }, isContainedWithinObject: function (e) { for (var i = t(this.oCoords), r = 0; r < 4; r++)
            if (!e.containsPoint(i[r]))
                return !1; return !0; }, isContainedWithinRect: function (t, e) { var i = this.getBoundingRect(); return i.left >= t.x && i.left + i.width <= e.x && i.top >= t.y && i.top + i.height <= e.y; }, containsPoint: function (t) { this.oCoords || this.setCoords(); var e = this._getImageLines(this.oCoords), i = this._findCrossPoints(t, e); return 0 !== i && i % 2 === 1; }, _getImageLines: function (t) { return { topline: { o: t.tl, d: t.tr }, rightline: { o: t.tr, d: t.br }, bottomline: { o: t.br, d: t.bl }, leftline: { o: t.bl, d: t.tl } }; }, _findCrossPoints: function (t, e) { var i, r, n, s, o, a, h = 0; for (var c in e)
            if (a = e[c], !(a.o.y < t.y && a.d.y < t.y || a.o.y >= t.y && a.d.y >= t.y || (a.o.x === a.d.x && a.o.x >= t.x ? o = a.o.x : (i = 0, r = (a.d.y - a.o.y) / (a.d.x - a.o.x), n = t.y - i * t.x, s = a.o.y - r * a.o.x, o = -(n - s) / (i - r)), o >= t.x && (h += 1), 2 !== h)))
                break; return h; }, getBoundingRectWidth: function () { return this.getBoundingRect().width; }, getBoundingRectHeight: function () { return this.getBoundingRect().height; }, getBoundingRect: function () { return this.oCoords || this.setCoords(), fabric.util.makeBoundingBoxFromPoints([this.oCoords.tl, this.oCoords.tr, this.oCoords.br, this.oCoords.bl]); }, getWidth: function () { return this._getTransformedDimensions().x; }, getHeight: function () { return this._getTransformedDimensions().y; }, _constrainScale: function (t) { return Math.abs(t) < this.minScaleLimit ? t < 0 ? -this.minScaleLimit : this.minScaleLimit : t; }, scale: function (t) { return t = this._constrainScale(t), t < 0 && (this.flipX = !this.flipX, this.flipY = !this.flipY, t *= -1), this.scaleX = t, this.scaleY = t, this.setCoords(), this; }, scaleToWidth: function (t) { var e = this.getBoundingRect().width / this.getWidth(); return this.scale(t / this.width / e); }, scaleToHeight: function (t) { var e = this.getBoundingRect().height / this.getHeight(); return this.scale(t / this.height / e); }, setCoords: function () { var t = e(this.angle), i = this.getViewportTransform(), r = this._calculateCurrentDimensions(), n = r.x, s = r.y; n < 0 && (n = Math.abs(n)); var o = Math.sin(t), a = Math.cos(t), h = n > 0 ? Math.atan(s / n) : 0, c = n / Math.cos(h) / 2, l = Math.cos(h + t) * c, u = Math.sin(h + t) * c, f = fabric.util.transformPoint(this.getCenterPoint(), i), d = new fabric.Point(f.x - l, f.y - u), g = new fabric.Point(d.x + n * a, d.y + n * o), p = new fabric.Point(d.x - s * o, d.y + s * a), v = new fabric.Point(f.x + l, f.y + u), b = new fabric.Point((d.x + p.x) / 2, (d.y + p.y) / 2), m = new fabric.Point((g.x + d.x) / 2, (g.y + d.y) / 2), y = new fabric.Point((v.x + g.x) / 2, (v.y + g.y) / 2), _ = new fabric.Point((v.x + p.x) / 2, (v.y + p.y) / 2), x = new fabric.Point(m.x + o * this.rotatingPointOffset, m.y - a * this.rotatingPointOffset); return this.oCoords = { tl: d, tr: g, br: v, bl: p, ml: b, mt: m, mr: y, mb: _, mtr: x }, this._setCornerCoords && this._setCornerCoords(), this; }, _calcRotateMatrix: function () {
            if (this.angle) {
                var t = e(this.angle), i = Math.cos(t), r = Math.sin(t);
                return [i, r, -r, i, 0, 0];
            }
            return [1, 0, 0, 1, 0, 0];
        }, calcTransformMatrix: function () { var t = this.getCenterPoint(), e = [1, 0, 0, 1, t.x, t.y], r = this._calcRotateMatrix(), n = this._calcDimensionsTransformMatrix(this.skewX, this.skewY, !0), s = this.group ? this.group.calcTransformMatrix() : [1, 0, 0, 1, 0, 0]; return s = i(s, e), s = i(s, r), s = i(s, n); }, _calcDimensionsTransformMatrix: function (t, r, n) { var s = [1, 0, Math.tan(e(t)), 1], o = [1, Math.tan(e(r)), 0, 1], a = this.scaleX * (n && this.flipX ? -1 : 1), h = this.scaleY * (n && this.flipY ? -1 : 1), c = [a, 0, 0, h], l = i(c, s, !0); return i(l, o, !0); } });
}(), fabric.util.object.extend(fabric.Object.prototype, { sendToBack: function () { return this.group ? fabric.StaticCanvas.prototype.sendToBack.call(this.group, this) : this.canvas.sendToBack(this), this; }, bringToFront: function () { return this.group ? fabric.StaticCanvas.prototype.bringToFront.call(this.group, this) : this.canvas.bringToFront(this), this; }, sendBackwards: function (t) { return this.group ? fabric.StaticCanvas.prototype.sendBackwards.call(this.group, this, t) : this.canvas.sendBackwards(this, t), this; }, bringForward: function (t) { return this.group ? fabric.StaticCanvas.prototype.bringForward.call(this.group, this, t) : this.canvas.bringForward(this, t), this; }, moveTo: function (t) { return this.group ? fabric.StaticCanvas.prototype.moveTo.call(this.group, this, t) : this.canvas.moveTo(this, t), this; } }), function () { function t(t, e) { if (e) {
    if (e.toLive)
        return t + ": url(#SVGID_" + e.id + "); ";
    var i = new fabric.Color(e), r = t + ": " + i.toRgb() + "; ", n = i.getAlpha();
    return 1 !== n && (r += t + "-opacity: " + n.toString() + "; "), r;
} return t + ": none; "; } fabric.util.object.extend(fabric.Object.prototype, { getSvgStyles: function (e) { var i = this.fillRule, r = this.strokeWidth ? this.strokeWidth : "0", n = this.strokeDashArray ? this.strokeDashArray.join(" ") : "none", s = this.strokeLineCap ? this.strokeLineCap : "butt", o = this.strokeLineJoin ? this.strokeLineJoin : "miter", a = this.strokeMiterLimit ? this.strokeMiterLimit : "4", h = "undefined" != typeof this.opacity ? this.opacity : "1", c = this.visible ? "" : " visibility: hidden;", l = e ? "" : this.getSvgFilter(), u = t("fill", this.fill), f = t("stroke", this.stroke); return [f, "stroke-width: ", r, "; ", "stroke-dasharray: ", n, "; ", "stroke-linecap: ", s, "; ", "stroke-linejoin: ", o, "; ", "stroke-miterlimit: ", a, "; ", u, "fill-rule: ", i, "; ", "opacity: ", h, ";", l, c].join(""); }, getSvgFilter: function () { return this.shadow ? "filter: url(#SVGID_" + this.shadow.id + ");" : ""; }, getSvgId: function () { return this.id ? 'id="' + this.id + '" ' : ""; }, getSvgTransform: function () { if (this.group && "path-group" === this.group.type)
        return ""; var t = fabric.util.toFixed, e = this.getAngle(), i = this.getSkewX() % 360, r = this.getSkewY() % 360, n = this.getCenterPoint(), s = fabric.Object.NUM_FRACTION_DIGITS, o = "path-group" === this.type ? "" : "translate(" + t(n.x, s) + " " + t(n.y, s) + ")", a = 0 !== e ? " rotate(" + t(e, s) + ")" : "", h = 1 === this.scaleX && 1 === this.scaleY ? "" : " scale(" + t(this.scaleX, s) + " " + t(this.scaleY, s) + ")", c = 0 !== i ? " skewX(" + t(i, s) + ")" : "", l = 0 !== r ? " skewY(" + t(r, s) + ")" : "", u = "path-group" === this.type ? this.width : 0, f = this.flipX ? " matrix(-1 0 0 1 " + u + " 0) " : "", d = "path-group" === this.type ? this.height : 0, g = this.flipY ? " matrix(1 0 0 -1 0 " + d + ")" : ""; return [o, a, h, f, g, c, l].join(""); }, getSvgTransformMatrix: function () { return this.transformMatrix ? " matrix(" + this.transformMatrix.join(" ") + ") " : ""; }, _createBaseSVGMarkup: function () { var t = []; return this.fill && this.fill.toLive && t.push(this.fill.toSVG(this, !1)), this.stroke && this.stroke.toLive && t.push(this.stroke.toSVG(this, !1)), this.shadow && t.push(this.shadow.toSVG(this)), t; } }); }(), function () { function t(t, e, r) { var n = {}, s = !0; r.forEach(function (e) { n[e] = t[e]; }), i(t[e], n, s); } function e(t, i) { if (!fabric.isLikelyNode && t instanceof Element)
    return t === i; if (t instanceof Array) {
    if (t.length !== i.length)
        return !1;
    var r = i.concat().sort(), n = t.concat().sort();
    return !n.some(function (t, i) { return !e(r[i], t); });
} if (t instanceof Object) {
    for (var s in t)
        if (!e(t[s], i[s]))
            return !1;
    return !0;
} return t === i; } var i = fabric.util.object.extend; fabric.util.object.extend(fabric.Object.prototype, { hasStateChanged: function () { return !e(this.originalState, this); }, saveState: function (e) { return t(this, "originalState", this.stateProperties), e && e.stateProperties && t(this, "originalState", e.stateProperties), this; }, setupState: function (t) { return this.originalState = {}, this.saveState(t), this; } }); }(), function () { var t = fabric.util.degreesToRadians, e = function () { return "undefined" != typeof G_vmlCanvasManager; }; fabric.util.object.extend(fabric.Object.prototype, { _controlsVisibility: null, _findTargetCorner: function (t) { if (!this.hasControls || !this.active)
        return !1; var e, i, r = t.x, n = t.y; this.__corner = 0; for (var s in this.oCoords)
        if (this.isControlVisible(s) && ("mtr" !== s || this.hasRotatingPoint) && (!this.get("lockUniScaling") || "mt" !== s && "mr" !== s && "mb" !== s && "ml" !== s) && (i = this._getImageLines(this.oCoords[s].corner), e = this._findCrossPoints({ x: r, y: n }, i), 0 !== e && e % 2 === 1))
            return this.__corner = s, s; return !1; }, _setCornerCoords: function () { var e, i, r = this.oCoords, n = t(45 - this.angle), s = .707106 * this.cornerSize, o = s * Math.cos(n), a = s * Math.sin(n); for (var h in r)
        e = r[h].x, i = r[h].y, r[h].corner = { tl: { x: e - a, y: i - o }, tr: { x: e + o, y: i - a }, bl: { x: e - o, y: i + a }, br: { x: e + a, y: i + o } }; }, _getNonTransformedDimensions: function () { var t = this.strokeWidth, e = this.width, i = this.height, r = !0, n = !0; return "line" === this.type && "butt" === this.strokeLineCap && (n = e, r = i), n && (i += i < 0 ? -t : t), r && (e += e < 0 ? -t : t), { x: e, y: i }; }, _getTransformedDimensions: function (t, e) { "undefined" == typeof t && (t = this.skewX), "undefined" == typeof e && (e = this.skewY); var i, r, n = this._getNonTransformedDimensions(), s = n.x / 2, o = n.y / 2, a = [{ x: -s, y: -o }, { x: s, y: -o }, { x: -s, y: o }, { x: s, y: o }], h = this._calcDimensionsTransformMatrix(t, e, !1); for (i = 0; i < a.length; i++)
        a[i] = fabric.util.transformPoint(a[i], h); return r = fabric.util.makeBoundingBoxFromPoints(a), { x: r.width, y: r.height }; }, _calculateCurrentDimensions: function () { var t = this.getViewportTransform(), e = this._getTransformedDimensions(), i = e.x, r = e.y, n = fabric.util.transformPoint(new fabric.Point(i, r), t, !0); return n.scalarAdd(2 * this.padding); }, drawSelectionBackground: function (e) { if (!this.selectionBackgroundColor || this.group || !this.active)
        return this; e.save(); var i = this.getCenterPoint(), r = this._calculateCurrentDimensions(), n = this.canvas.viewportTransform; return e.translate(i.x, i.y), e.scale(1 / n[0], 1 / n[3]), e.rotate(t(this.angle)), e.fillStyle = this.selectionBackgroundColor, e.fillRect(-r.x / 2, -r.y / 2, r.x, r.y), e.restore(), this; }, drawBorders: function (t) { if (!this.hasBorders)
        return this; var e = this._calculateCurrentDimensions(), i = 1 / this.borderScaleFactor, r = e.x + i, n = e.y + i; if (t.save(), t.strokeStyle = this.borderColor, this._setLineDash(t, this.borderDashArray, null), t.strokeRect(-r / 2, -n / 2, r, n), this.hasRotatingPoint && this.isControlVisible("mtr") && !this.get("lockRotation") && this.hasControls) {
        var s = -n / 2;
        t.beginPath(), t.moveTo(0, s), t.lineTo(0, s - this.rotatingPointOffset), t.closePath(), t.stroke();
    } return t.restore(), this; }, drawBordersInGroup: function (t, e) { if (!this.hasBorders)
        return this; var i = this._getNonTransformedDimensions(), r = fabric.util.customTransformMatrix(e.scaleX, e.scaleY, e.skewX), n = fabric.util.transformPoint(i, r), s = 1 / this.borderScaleFactor, o = n.x + s + 2 * this.padding, a = n.y + s + 2 * this.padding; return t.save(), this._setLineDash(t, this.borderDashArray, null), t.strokeStyle = this.borderColor, t.strokeRect(-o / 2, -a / 2, o, a), t.restore(), this; }, drawControls: function (t) { if (!this.hasControls)
        return this; var e = this._calculateCurrentDimensions(), i = e.x, r = e.y, n = this.cornerSize, s = -(i + n) / 2, o = -(r + n) / 2, a = this.transparentCorners ? "stroke" : "fill"; return t.save(), t.strokeStyle = t.fillStyle = this.cornerColor, this.transparentCorners || (t.strokeStyle = this.cornerStrokeColor), this._setLineDash(t, this.cornerDashArray, null), this._drawControl("tl", t, a, s, o), this._drawControl("tr", t, a, s + i, o), this._drawControl("bl", t, a, s, o + r), this._drawControl("br", t, a, s + i, o + r), this.get("lockUniScaling") || (this._drawControl("mt", t, a, s + i / 2, o), this._drawControl("mb", t, a, s + i / 2, o + r), this._drawControl("mr", t, a, s + i, o + r / 2), this._drawControl("ml", t, a, s, o + r / 2)), this.hasRotatingPoint && this._drawControl("mtr", t, a, s + i / 2, o - this.rotatingPointOffset), t.restore(), this; }, _drawControl: function (t, i, r, n, s) { if (this.isControlVisible(t)) {
        var o = this.cornerSize, a = !this.transparentCorners && this.cornerStrokeColor;
        switch (this.cornerStyle) {
            case "circle":
                i.beginPath(), i.arc(n + o / 2, s + o / 2, o / 2, 0, 2 * Math.PI, !1), i[r](), a && i.stroke();
                break;
            default: e() || this.transparentCorners || i.clearRect(n, s, o, o), i[r + "Rect"](n, s, o, o), a && i.strokeRect(n, s, o, o);
        }
    } }, isControlVisible: function (t) { return this._getControlsVisibility()[t]; }, setControlVisible: function (t, e) { return this._getControlsVisibility()[t] = e, this; }, setControlsVisibility: function (t) { t || (t = {}); for (var e in t)
        this.setControlVisible(e, t[e]); return this; }, _getControlsVisibility: function () { return this._controlsVisibility || (this._controlsVisibility = { tl: !0, tr: !0, br: !0, bl: !0, ml: !0, mt: !0, mr: !0, mb: !0, mtr: !0 }), this._controlsVisibility; } }); }(), fabric.util.object.extend(fabric.StaticCanvas.prototype, { FX_DURATION: 500, fxCenterObjectH: function (t, e) { e = e || {}; var i = function () { }, r = e.onComplete || i, n = e.onChange || i, s = this; return fabric.util.animate({ startValue: t.get("left"), endValue: this.getCenter().left, duration: this.FX_DURATION, onChange: function (e) { t.set("left", e), s.renderAll(), n(); }, onComplete: function () { t.setCoords(), r(); } }), this; }, fxCenterObjectV: function (t, e) { e = e || {}; var i = function () { }, r = e.onComplete || i, n = e.onChange || i, s = this; return fabric.util.animate({ startValue: t.get("top"), endValue: this.getCenter().top, duration: this.FX_DURATION, onChange: function (e) { t.set("top", e), s.renderAll(), n(); }, onComplete: function () { t.setCoords(), r(); } }), this; }, fxRemove: function (t, e) { e = e || {}; var i = function () { }, r = e.onComplete || i, n = e.onChange || i, s = this; return fabric.util.animate({ startValue: t.get("opacity"), endValue: 0, duration: this.FX_DURATION, onStart: function () { t.set("active", !1); }, onChange: function (e) { t.set("opacity", e), s.renderAll(), n(); }, onComplete: function () { s.remove(t), r(); } }), this; } }), fabric.util.object.extend(fabric.Object.prototype, { animate: function () { if (arguments[0] && "object" == typeof arguments[0]) {
        var t, e, i = [];
        for (t in arguments[0])
            i.push(t);
        for (var r = 0, n = i.length; r < n; r++)
            t = i[r], e = r !== n - 1, this._animate(t, arguments[0][t], arguments[1], e);
    }
    else
        this._animate.apply(this, arguments); return this; }, _animate: function (t, e, i, r) { var n, s = this; e = e.toString(), i = i ? fabric.util.object.clone(i) : {}, ~t.indexOf(".") && (n = t.split(".")); var o = n ? this.get(n[0])[n[1]] : this.get(t); "from" in i || (i.from = o), e = ~e.indexOf("=") ? o + parseFloat(e.replace("=", "")) : parseFloat(e), fabric.util.animate({ startValue: i.from, endValue: e, byValue: i.by, easing: i.easing, duration: i.duration, abort: i.abort && function () { return i.abort.call(s); }, onChange: function (e) { n ? s[n[0]][n[1]] = e : s.set(t, e), r || i.onChange && i.onChange(); }, onComplete: function () { r || (s.setCoords(), i.onComplete && i.onComplete()); } }); } }), function (t) {
    "use strict";
    function e(t, e) { var i = t.origin, r = t.axis1, n = t.axis2, s = t.dimension, o = e.nearest, a = e.center, h = e.farthest; return function () { switch (this.get(i)) {
        case o: return Math.min(this.get(r), this.get(n));
        case a: return Math.min(this.get(r), this.get(n)) + .5 * this.get(s);
        case h: return Math.max(this.get(r), this.get(n));
    } }; }
    var i = t.fabric || (t.fabric = {}), r = i.util.object.extend, n = { x1: 1, x2: 1, y1: 1, y2: 1 }, s = i.StaticCanvas.supports("setLineDash");
    return i.Line ? void i.warn("fabric.Line is already defined") : (i.Line = i.util.createClass(i.Object, { type: "line", x1: 0, y1: 0, x2: 0, y2: 0, initialize: function (t, e) { e = e || {}, t || (t = [0, 0, 0, 0]), this.callSuper("initialize", e), this.set("x1", t[0]), this.set("y1", t[1]), this.set("x2", t[2]), this.set("y2", t[3]), this._setWidthHeight(e); }, _setWidthHeight: function (t) { t || (t = {}), this.width = Math.abs(this.x2 - this.x1), this.height = Math.abs(this.y2 - this.y1), this.left = "left" in t ? t.left : this._getLeftToOriginX(), this.top = "top" in t ? t.top : this._getTopToOriginY(); }, _set: function (t, e) { return this.callSuper("_set", t, e), "undefined" != typeof n[t] && this._setWidthHeight(), this; }, _getLeftToOriginX: e({ origin: "originX", axis1: "x1", axis2: "x2", dimension: "width" }, { nearest: "left", center: "center", farthest: "right" }), _getTopToOriginY: e({ origin: "originY", axis1: "y1", axis2: "y2", dimension: "height" }, { nearest: "top", center: "center", farthest: "bottom" }), _render: function (t, e) { if (t.beginPath(), e) {
            var i = this.getCenterPoint();
            t.translate(i.x - this.strokeWidth / 2, i.y - this.strokeWidth / 2);
        } if (!this.strokeDashArray || this.strokeDashArray && s) {
            var r = this.calcLinePoints();
            t.moveTo(r.x1, r.y1), t.lineTo(r.x2, r.y2);
        } t.lineWidth = this.strokeWidth; var n = t.strokeStyle; t.strokeStyle = this.stroke || t.fillStyle, this.stroke && this._renderStroke(t), t.strokeStyle = n; }, _renderDashedStroke: function (t) { var e = this.calcLinePoints(); t.beginPath(), i.util.drawDashedLine(t, e.x1, e.y1, e.x2, e.y2, this.strokeDashArray), t.closePath(); }, toObject: function (t) { return r(this.callSuper("toObject", t), this.calcLinePoints()); }, calcLinePoints: function () { var t = this.x1 <= this.x2 ? -1 : 1, e = this.y1 <= this.y2 ? -1 : 1, i = t * this.width * .5, r = e * this.height * .5, n = t * this.width * -.5, s = e * this.height * -.5; return { x1: i, x2: n, y1: r, y2: s }; }, toSVG: function (t) { var e = this._createBaseSVGMarkup(), i = { x1: this.x1, x2: this.x2, y1: this.y1, y2: this.y2 }; return this.group && "path-group" === this.group.type || (i = this.calcLinePoints()), e.push("<line ", this.getSvgId(), 'x1="', i.x1, '" y1="', i.y1, '" x2="', i.x2, '" y2="', i.y2, '" style="', this.getSvgStyles(), '" transform="', this.getSvgTransform(), this.getSvgTransformMatrix(), '"/>\n'), t ? t(e.join("")) : e.join(""); }, complexity: function () { return 1; } }), i.Line.ATTRIBUTE_NAMES = i.SHARED_ATTRIBUTES.concat("x1 y1 x2 y2".split(" ")), i.Line.fromElement = function (t, e) { var n = i.parseAttributes(t, i.Line.ATTRIBUTE_NAMES), s = [n.x1 || 0, n.y1 || 0, n.x2 || 0, n.y2 || 0]; return new i.Line(s, r(n, e)); }, void (i.Line.fromObject = function (t, e) { var r = [t.x1, t.y1, t.x2, t.y2], n = new i.Line(r, t); return e && e(n), n; }));
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    function e(t) { return "radius" in t && t.radius >= 0; }
    var i = t.fabric || (t.fabric = {}), r = Math.PI, n = i.util.object.extend;
    return i.Circle ? void i.warn("fabric.Circle is already defined.") : (i.Circle = i.util.createClass(i.Object, { type: "circle", radius: 0, startAngle: 0, endAngle: 2 * r, initialize: function (t) { t = t || {}, this.callSuper("initialize", t), this.set("radius", t.radius || 0), this.startAngle = t.startAngle || this.startAngle, this.endAngle = t.endAngle || this.endAngle; }, _set: function (t, e) { return this.callSuper("_set", t, e), "radius" === t && this.setRadius(e), this; }, toObject: function (t) { return n(this.callSuper("toObject", t), { radius: this.get("radius"), startAngle: this.startAngle, endAngle: this.endAngle }); }, toSVG: function (t) { var e = this._createBaseSVGMarkup(), i = 0, n = 0, s = (this.endAngle - this.startAngle) % (2 * r); if (0 === s)
            this.group && "path-group" === this.group.type && (i = this.left + this.radius, n = this.top + this.radius), e.push("<circle ", this.getSvgId(), 'cx="' + i + '" cy="' + n + '" ', 'r="', this.radius, '" style="', this.getSvgStyles(), '" transform="', this.getSvgTransform(), " ", this.getSvgTransformMatrix(), '"/>\n');
        else {
            var o = Math.cos(this.startAngle) * this.radius, a = Math.sin(this.startAngle) * this.radius, h = Math.cos(this.endAngle) * this.radius, c = Math.sin(this.endAngle) * this.radius, l = s > r ? "1" : "0";
            e.push('<path d="M ' + o + " " + a, " A " + this.radius + " " + this.radius, " 0 ", +l + " 1", " " + h + " " + c, '" style="', this.getSvgStyles(), '" transform="', this.getSvgTransform(), " ", this.getSvgTransformMatrix(), '"/>\n');
        } return t ? t(e.join("")) : e.join(""); }, _render: function (t, e) { t.beginPath(), t.arc(e ? this.left + this.radius : 0, e ? this.top + this.radius : 0, this.radius, this.startAngle, this.endAngle, !1), this._renderFill(t), this._renderStroke(t); }, getRadiusX: function () { return this.get("radius") * this.get("scaleX"); }, getRadiusY: function () { return this.get("radius") * this.get("scaleY"); }, setRadius: function (t) { return this.radius = t, this.set("width", 2 * t).set("height", 2 * t); }, complexity: function () { return 1; } }), i.Circle.ATTRIBUTE_NAMES = i.SHARED_ATTRIBUTES.concat("cx cy r".split(" ")), i.Circle.fromElement = function (t, r) { r || (r = {}); var s = i.parseAttributes(t, i.Circle.ATTRIBUTE_NAMES); if (!e(s))
        throw new Error("value of `r` attribute is required and can not be negative"); s.left = s.left || 0, s.top = s.top || 0; var o = new i.Circle(n(s, r)); return o.left -= o.radius, o.top -= o.radius, o; }, void (i.Circle.fromObject = function (t, e) { var r = new i.Circle(t); return e && e(r), r; }));
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {});
    return e.Triangle ? void e.warn("fabric.Triangle is already defined") : (e.Triangle = e.util.createClass(e.Object, { type: "triangle", initialize: function (t) { t = t || {}, this.callSuper("initialize", t), this.set("width", t.width || 100).set("height", t.height || 100); }, _render: function (t) { var e = this.width / 2, i = this.height / 2; t.beginPath(), t.moveTo(-e, i), t.lineTo(0, -i), t.lineTo(e, i), t.closePath(), this._renderFill(t), this._renderStroke(t); }, _renderDashedStroke: function (t) { var i = this.width / 2, r = this.height / 2; t.beginPath(), e.util.drawDashedLine(t, -i, r, 0, -r, this.strokeDashArray), e.util.drawDashedLine(t, 0, -r, i, r, this.strokeDashArray), e.util.drawDashedLine(t, i, r, -i, r, this.strokeDashArray), t.closePath(); }, toSVG: function (t) { var e = this._createBaseSVGMarkup(), i = this.width / 2, r = this.height / 2, n = [-i + " " + r, "0 " + -r, i + " " + r].join(","); return e.push("<polygon ", this.getSvgId(), 'points="', n, '" style="', this.getSvgStyles(), '" transform="', this.getSvgTransform(), '"/>'), t ? t(e.join("")) : e.join(""); }, complexity: function () { return 1; } }), void (e.Triangle.fromObject = function (t, i) { var r = new e.Triangle(t); return i && i(r), r; }));
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = 2 * Math.PI, r = e.util.object.extend;
    return e.Ellipse ? void e.warn("fabric.Ellipse is already defined.") : (e.Ellipse = e.util.createClass(e.Object, { type: "ellipse", rx: 0, ry: 0, initialize: function (t) { t = t || {}, this.callSuper("initialize", t), this.set("rx", t.rx || 0), this.set("ry", t.ry || 0); }, _set: function (t, e) { switch (this.callSuper("_set", t, e), t) {
            case "rx":
                this.rx = e, this.set("width", 2 * e);
                break;
            case "ry": this.ry = e, this.set("height", 2 * e);
        } return this; }, getRx: function () { return this.get("rx") * this.get("scaleX"); }, getRy: function () { return this.get("ry") * this.get("scaleY"); }, toObject: function (t) { return r(this.callSuper("toObject", t), { rx: this.get("rx"), ry: this.get("ry") }); }, toSVG: function (t) { var e = this._createBaseSVGMarkup(), i = 0, r = 0; return this.group && "path-group" === this.group.type && (i = this.left + this.rx, r = this.top + this.ry), e.push("<ellipse ", this.getSvgId(), 'cx="', i, '" cy="', r, '" ', 'rx="', this.rx, '" ry="', this.ry, '" style="', this.getSvgStyles(), '" transform="', this.getSvgTransform(), this.getSvgTransformMatrix(), '"/>\n'), t ? t(e.join("")) : e.join(""); }, _render: function (t, e) { t.beginPath(), t.save(), t.transform(1, 0, 0, this.ry / this.rx, 0, 0), t.arc(e ? this.left + this.rx : 0, e ? (this.top + this.ry) * this.rx / this.ry : 0, this.rx, 0, i, !1), t.restore(), this._renderFill(t), this._renderStroke(t); }, complexity: function () { return 1; } }), e.Ellipse.ATTRIBUTE_NAMES = e.SHARED_ATTRIBUTES.concat("cx cy rx ry".split(" ")), e.Ellipse.fromElement = function (t, i) { i || (i = {}); var n = e.parseAttributes(t, e.Ellipse.ATTRIBUTE_NAMES); n.left = n.left || 0, n.top = n.top || 0; var s = new e.Ellipse(r(n, i)); return s.top -= s.ry, s.left -= s.rx, s; }, void (e.Ellipse.fromObject = function (t, i) { var r = new e.Ellipse(t); return i && i(r), r; }));
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = e.util.object.extend;
    if (e.Rect)
        return void e.warn("fabric.Rect is already defined");
    var r = e.Object.prototype.stateProperties.concat();
    r.push("rx", "ry", "x", "y"), e.Rect = e.util.createClass(e.Object, { stateProperties: r, type: "rect", rx: 0, ry: 0, strokeDashArray: null, initialize: function (t) { t = t || {}, this.callSuper("initialize", t), this._initRxRy(); }, _initRxRy: function () { this.rx && !this.ry ? this.ry = this.rx : this.ry && !this.rx && (this.rx = this.ry); }, _render: function (t, e) { if (1 === this.width && 1 === this.height)
            return void t.fillRect(-.5, -.5, 1, 1); var i = this.rx ? Math.min(this.rx, this.width / 2) : 0, r = this.ry ? Math.min(this.ry, this.height / 2) : 0, n = this.width, s = this.height, o = e ? this.left : -this.width / 2, a = e ? this.top : -this.height / 2, h = 0 !== i || 0 !== r, c = .4477152502; t.beginPath(), t.moveTo(o + i, a), t.lineTo(o + n - i, a), h && t.bezierCurveTo(o + n - c * i, a, o + n, a + c * r, o + n, a + r), t.lineTo(o + n, a + s - r), h && t.bezierCurveTo(o + n, a + s - c * r, o + n - c * i, a + s, o + n - i, a + s), t.lineTo(o + i, a + s), h && t.bezierCurveTo(o + c * i, a + s, o, a + s - c * r, o, a + s - r), t.lineTo(o, a + r), h && t.bezierCurveTo(o, a + c * r, o + c * i, a, o + i, a), t.closePath(), this._renderFill(t), this._renderStroke(t); }, _renderDashedStroke: function (t) { var i = -this.width / 2, r = -this.height / 2, n = this.width, s = this.height; t.beginPath(), e.util.drawDashedLine(t, i, r, i + n, r, this.strokeDashArray), e.util.drawDashedLine(t, i + n, r, i + n, r + s, this.strokeDashArray), e.util.drawDashedLine(t, i + n, r + s, i, r + s, this.strokeDashArray), e.util.drawDashedLine(t, i, r + s, i, r, this.strokeDashArray), t.closePath(); }, toObject: function (t) { var e = i(this.callSuper("toObject", t), { rx: this.get("rx") || 0, ry: this.get("ry") || 0 }); return this.includeDefaultValues || this._removeDefaultValues(e), e; }, toSVG: function (t) { var e = this._createBaseSVGMarkup(), i = this.left, r = this.top; return this.group && "path-group" === this.group.type || (i = -this.width / 2, r = -this.height / 2), e.push("<rect ", this.getSvgId(), 'x="', i, '" y="', r, '" rx="', this.get("rx"), '" ry="', this.get("ry"), '" width="', this.width, '" height="', this.height, '" style="', this.getSvgStyles(), '" transform="', this.getSvgTransform(), this.getSvgTransformMatrix(), '"/>\n'), t ? t(e.join("")) : e.join(""); }, complexity: function () { return 1; } }), e.Rect.ATTRIBUTE_NAMES = e.SHARED_ATTRIBUTES.concat("x y rx ry width height".split(" ")), e.Rect.fromElement = function (t, r) { if (!t)
        return null; r = r || {}; var n = e.parseAttributes(t, e.Rect.ATTRIBUTE_NAMES); n.left = n.left || 0, n.top = n.top || 0; var s = new e.Rect(i(r ? e.util.object.clone(r) : {}, n)); return s.visible = s.visible && s.width > 0 && s.height > 0, s; }, e.Rect.fromObject = function (t, i) { var r = new e.Rect(t); return i && i(r), r; };
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {});
    return e.Polyline ? void e.warn("fabric.Polyline is already defined") : (e.Polyline = e.util.createClass(e.Object, { type: "polyline", points: null, minX: 0, minY: 0, initialize: function (t, i) { return e.Polygon.prototype.initialize.call(this, t, i); }, _calcDimensions: function () { return e.Polygon.prototype._calcDimensions.call(this); }, toObject: function (t) { return e.Polygon.prototype.toObject.call(this, t); }, toSVG: function (t) { return e.Polygon.prototype.toSVG.call(this, t); }, _render: function (t, i) { e.Polygon.prototype.commonRender.call(this, t, i) && (this._renderFill(t), this._renderStroke(t)); }, _renderDashedStroke: function (t) { var i, r; t.beginPath(); for (var n = 0, s = this.points.length; n < s; n++)
            i = this.points[n], r = this.points[n + 1] || i, e.util.drawDashedLine(t, i.x, i.y, r.x, r.y, this.strokeDashArray); }, complexity: function () { return this.get("points").length; } }), e.Polyline.ATTRIBUTE_NAMES = e.SHARED_ATTRIBUTES.concat(), e.Polyline.fromElement = function (t, i) { if (!t)
        return null; i || (i = {}); var r = e.parsePointsAttribute(t.getAttribute("points")), n = e.parseAttributes(t, e.Polyline.ATTRIBUTE_NAMES); return new e.Polyline(r, e.util.object.extend(n, i)); }, void (e.Polyline.fromObject = function (t, i) { var r = new e.Polyline(t.points, t); return i && i(r), r; }));
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = e.util.object.extend, r = e.util.array.min, n = e.util.array.max, s = e.util.toFixed;
    return e.Polygon ? void e.warn("fabric.Polygon is already defined") : (e.Polygon = e.util.createClass(e.Object, { type: "polygon", points: null, minX: 0, minY: 0, initialize: function (t, e) { e = e || {}, this.points = t || [], this.callSuper("initialize", e), this._calcDimensions(), "top" in e || (this.top = this.minY), "left" in e || (this.left = this.minX), this.pathOffset = { x: this.minX + this.width / 2, y: this.minY + this.height / 2 }; }, _calcDimensions: function () { var t = this.points, e = r(t, "x"), i = r(t, "y"), s = n(t, "x"), o = n(t, "y"); this.width = s - e || 0, this.height = o - i || 0, this.minX = e || 0, this.minY = i || 0; }, toObject: function (t) { return i(this.callSuper("toObject", t), { points: this.points.concat() }); }, toSVG: function (t) { for (var e, i = [], r = this._createBaseSVGMarkup(), n = 0, o = this.points.length; n < o; n++)
            i.push(s(this.points[n].x, 2), ",", s(this.points[n].y, 2), " "); return this.group && "path-group" === this.group.type || (e = " translate(" + -this.pathOffset.x + ", " + -this.pathOffset.y + ") "), r.push("<", this.type, " ", this.getSvgId(), 'points="', i.join(""), '" style="', this.getSvgStyles(), '" transform="', this.getSvgTransform(), e, " ", this.getSvgTransformMatrix(), '"/>\n'), t ? t(r.join("")) : r.join(""); }, _render: function (t, e) { this.commonRender(t, e) && (this._renderFill(t), (this.stroke || this.strokeDashArray) && (t.closePath(), this._renderStroke(t))); }, commonRender: function (t, e) { var i, r = this.points.length; if (!r || isNaN(this.points[r - 1].y))
            return !1; e || t.translate(-this.pathOffset.x, -this.pathOffset.y), t.beginPath(), t.moveTo(this.points[0].x, this.points[0].y); for (var n = 0; n < r; n++)
            i = this.points[n], t.lineTo(i.x, i.y); return !0; }, _renderDashedStroke: function (t) { e.Polyline.prototype._renderDashedStroke.call(this, t), t.closePath(); }, complexity: function () { return this.points.length; } }), e.Polygon.ATTRIBUTE_NAMES = e.SHARED_ATTRIBUTES.concat(), e.Polygon.fromElement = function (t, r) { if (!t)
        return null; r || (r = {}); var n = e.parsePointsAttribute(t.getAttribute("points")), s = e.parseAttributes(t, e.Polygon.ATTRIBUTE_NAMES); return new e.Polygon(n, i(s, r)); }, void (e.Polygon.fromObject = function (t, i) { var r = new e.Polygon(t.points, t); return i && i(r), r; }));
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = e.util.array.min, r = e.util.array.max, n = e.util.object.extend, s = Object.prototype.toString, o = e.util.drawArc, a = { m: 2, l: 2, h: 1, v: 1, c: 6, s: 4, q: 4, t: 2, a: 7 }, h = { m: "l", M: "L" };
    return e.Path ? void e.warn("fabric.Path is already defined") : (e.Path = e.util.createClass(e.Object, { type: "path", path: null, minX: 0, minY: 0, initialize: function (t, e) { e = e || {}, this.setOptions(e), t || (t = []); var i = "[object Array]" === s.call(t); this.path = i ? t : t.match && t.match(/[mzlhvcsqta][^mzlhvcsqta]*/gi), this.path && (i || (this.path = this._parsePath()), this._setPositionDimensions(e), e.sourcePath && this.setSourcePath(e.sourcePath)); }, _setPositionDimensions: function (t) { var e = this._parseDimensions(); this.minX = e.left, this.minY = e.top, this.width = e.width, this.height = e.height, "undefined" == typeof t.left && (this.left = e.left + ("center" === this.originX ? this.width / 2 : "right" === this.originX ? this.width : 0)), "undefined" == typeof t.top && (this.top = e.top + ("center" === this.originY ? this.height / 2 : "bottom" === this.originY ? this.height : 0)), this.pathOffset = this.pathOffset || { x: this.minX + this.width / 2, y: this.minY + this.height / 2 }; }, _renderPathCommands: function (t) { var e, i, r, n = null, s = 0, a = 0, h = 0, c = 0, l = 0, u = 0, f = -this.pathOffset.x, d = -this.pathOffset.y; this.group && "path-group" === this.group.type && (f = 0, d = 0), t.beginPath(); for (var g = 0, p = this.path.length; g < p; ++g) {
            switch (e = this.path[g], e[0]) {
                case "l":
                    h += e[1], c += e[2], t.lineTo(h + f, c + d);
                    break;
                case "L":
                    h = e[1], c = e[2], t.lineTo(h + f, c + d);
                    break;
                case "h":
                    h += e[1], t.lineTo(h + f, c + d);
                    break;
                case "H":
                    h = e[1], t.lineTo(h + f, c + d);
                    break;
                case "v":
                    c += e[1], t.lineTo(h + f, c + d);
                    break;
                case "V":
                    c = e[1], t.lineTo(h + f, c + d);
                    break;
                case "m":
                    h += e[1], c += e[2], s = h, a = c, t.moveTo(h + f, c + d);
                    break;
                case "M":
                    h = e[1], c = e[2], s = h, a = c, t.moveTo(h + f, c + d);
                    break;
                case "c":
                    i = h + e[5], r = c + e[6], l = h + e[3], u = c + e[4], t.bezierCurveTo(h + e[1] + f, c + e[2] + d, l + f, u + d, i + f, r + d), h = i, c = r;
                    break;
                case "C":
                    h = e[5], c = e[6], l = e[3], u = e[4], t.bezierCurveTo(e[1] + f, e[2] + d, l + f, u + d, h + f, c + d);
                    break;
                case "s":
                    i = h + e[3], r = c + e[4], null === n[0].match(/[CcSs]/) ? (l = h, u = c) : (l = 2 * h - l, u = 2 * c - u), t.bezierCurveTo(l + f, u + d, h + e[1] + f, c + e[2] + d, i + f, r + d), l = h + e[1], u = c + e[2], h = i, c = r;
                    break;
                case "S":
                    i = e[3], r = e[4], null === n[0].match(/[CcSs]/) ? (l = h, u = c) : (l = 2 * h - l, u = 2 * c - u), t.bezierCurveTo(l + f, u + d, e[1] + f, e[2] + d, i + f, r + d), h = i, c = r, l = e[1], u = e[2];
                    break;
                case "q":
                    i = h + e[3], r = c + e[4], l = h + e[1], u = c + e[2], t.quadraticCurveTo(l + f, u + d, i + f, r + d), h = i, c = r;
                    break;
                case "Q":
                    i = e[3], r = e[4], t.quadraticCurveTo(e[1] + f, e[2] + d, i + f, r + d), h = i, c = r, l = e[1], u = e[2];
                    break;
                case "t":
                    i = h + e[1], r = c + e[2], null === n[0].match(/[QqTt]/) ? (l = h, u = c) : (l = 2 * h - l, u = 2 * c - u), t.quadraticCurveTo(l + f, u + d, i + f, r + d), h = i, c = r;
                    break;
                case "T":
                    i = e[1], r = e[2], null === n[0].match(/[QqTt]/) ? (l = h, u = c) : (l = 2 * h - l, u = 2 * c - u), t.quadraticCurveTo(l + f, u + d, i + f, r + d), h = i, c = r;
                    break;
                case "a":
                    o(t, h + f, c + d, [e[1], e[2], e[3], e[4], e[5], e[6] + h + f, e[7] + c + d]), h += e[6], c += e[7];
                    break;
                case "A":
                    o(t, h + f, c + d, [e[1], e[2], e[3], e[4], e[5], e[6] + f, e[7] + d]), h = e[6], c = e[7];
                    break;
                case "z":
                case "Z": h = s, c = a, t.closePath();
            }
            n = e;
        } }, _render: function (t) { this._renderPathCommands(t), this._renderFill(t), this._renderStroke(t); }, toString: function () { return "#<fabric.Path (" + this.complexity() + '): { "top": ' + this.top + ', "left": ' + this.left + " }>"; }, toObject: function (t) { var e = n(this.callSuper("toObject", t), { path: this.path.map(function (t) { return t.slice(); }), pathOffset: this.pathOffset }); return this.sourcePath && (e.sourcePath = this.sourcePath), this.transformMatrix && (e.transformMatrix = this.transformMatrix), e; }, toDatalessObject: function (t) { var e = this.toObject(t); return this.sourcePath && (e.path = this.sourcePath), delete e.sourcePath, e; }, toSVG: function (t) { for (var e = [], i = this._createBaseSVGMarkup(), r = "", n = 0, s = this.path.length; n < s; n++)
            e.push(this.path[n].join(" ")); var o = e.join(" "); return this.group && "path-group" === this.group.type || (r = " translate(" + -this.pathOffset.x + ", " + -this.pathOffset.y + ") "), i.push("<path ", this.getSvgId(), 'd="', o, '" style="', this.getSvgStyles(), '" transform="', this.getSvgTransform(), r, this.getSvgTransformMatrix(), '" stroke-linecap="round" ', "/>\n"), t ? t(i.join("")) : i.join(""); }, complexity: function () { return this.path.length; }, _parsePath: function () { for (var t, e, i, r, n, s = [], o = [], c = /([-+]?((\d+\.\d+)|((\d+)|(\.\d+)))(?:e[-+]?\d+)?)/gi, l = 0, u = this.path.length; l < u; l++) {
            for (t = this.path[l], r = t.slice(1).trim(), o.length = 0; i = c.exec(r);)
                o.push(i[0]);
            n = [t.charAt(0)];
            for (var f = 0, d = o.length; f < d; f++)
                e = parseFloat(o[f]), isNaN(e) || n.push(e);
            var g = n[0], p = a[g.toLowerCase()], v = h[g] || g;
            if (n.length - 1 > p)
                for (var b = 1, m = n.length; b < m; b += p)
                    s.push([g].concat(n.slice(b, b + p))), g = v;
            else
                s.push(n);
        } return s; }, _parseDimensions: function () { for (var t, n, s, o, a = [], h = [], c = null, l = 0, u = 0, f = 0, d = 0, g = 0, p = 0, v = 0, b = this.path.length; v < b; ++v) {
            switch (t = this.path[v], t[0]) {
                case "l":
                    f += t[1], d += t[2], o = [];
                    break;
                case "L":
                    f = t[1], d = t[2], o = [];
                    break;
                case "h":
                    f += t[1], o = [];
                    break;
                case "H":
                    f = t[1], o = [];
                    break;
                case "v":
                    d += t[1], o = [];
                    break;
                case "V":
                    d = t[1], o = [];
                    break;
                case "m":
                    f += t[1], d += t[2], l = f, u = d, o = [];
                    break;
                case "M":
                    f = t[1], d = t[2], l = f, u = d, o = [];
                    break;
                case "c":
                    n = f + t[5], s = d + t[6], g = f + t[3], p = d + t[4], o = e.util.getBoundsOfCurve(f, d, f + t[1], d + t[2], g, p, n, s), f = n, d = s;
                    break;
                case "C":
                    f = t[5], d = t[6], g = t[3], p = t[4], o = e.util.getBoundsOfCurve(f, d, t[1], t[2], g, p, f, d);
                    break;
                case "s":
                    n = f + t[3], s = d + t[4], null === c[0].match(/[CcSs]/) ? (g = f, p = d) : (g = 2 * f - g, p = 2 * d - p), o = e.util.getBoundsOfCurve(f, d, g, p, f + t[1], d + t[2], n, s), g = f + t[1], p = d + t[2], f = n, d = s;
                    break;
                case "S":
                    n = t[3], s = t[4], null === c[0].match(/[CcSs]/) ? (g = f, p = d) : (g = 2 * f - g, p = 2 * d - p), o = e.util.getBoundsOfCurve(f, d, g, p, t[1], t[2], n, s), f = n, d = s, g = t[1], p = t[2];
                    break;
                case "q":
                    n = f + t[3], s = d + t[4], g = f + t[1], p = d + t[2], o = e.util.getBoundsOfCurve(f, d, g, p, g, p, n, s), f = n, d = s;
                    break;
                case "Q":
                    g = t[1], p = t[2], o = e.util.getBoundsOfCurve(f, d, g, p, g, p, t[3], t[4]), f = t[3], d = t[4];
                    break;
                case "t":
                    n = f + t[1], s = d + t[2], null === c[0].match(/[QqTt]/) ? (g = f, p = d) : (g = 2 * f - g, p = 2 * d - p), o = e.util.getBoundsOfCurve(f, d, g, p, g, p, n, s), f = n, d = s;
                    break;
                case "T":
                    n = t[1], s = t[2], null === c[0].match(/[QqTt]/) ? (g = f, p = d) : (g = 2 * f - g, p = 2 * d - p), o = e.util.getBoundsOfCurve(f, d, g, p, g, p, n, s), f = n, d = s;
                    break;
                case "a":
                    o = e.util.getBoundsOfArc(f, d, t[1], t[2], t[3], t[4], t[5], t[6] + f, t[7] + d), f += t[6], d += t[7];
                    break;
                case "A":
                    o = e.util.getBoundsOfArc(f, d, t[1], t[2], t[3], t[4], t[5], t[6], t[7]), f = t[6], d = t[7];
                    break;
                case "z":
                case "Z": f = l, d = u;
            }
            c = t, o.forEach(function (t) { a.push(t.x), h.push(t.y); }), a.push(f), h.push(d);
        } var m = i(a) || 0, y = i(h) || 0, _ = r(a) || 0, x = r(h) || 0, S = _ - m, C = x - y, w = { left: m, top: y, width: S, height: C }; return w; } }), e.Path.fromObject = function (t, i) { var r; return "string" != typeof t.path ? (r = new e.Path(t.path, t), i && i(r), r) : void e.loadSVGFromURL(t.path, function (n) { var s = t.path; r = n[0], delete t.path, e.util.object.extend(r, t), r.setSourcePath(s), i && i(r); }); }, e.Path.ATTRIBUTE_NAMES = e.SHARED_ATTRIBUTES.concat(["d"]), e.Path.fromElement = function (t, i, r) { var s = e.parseAttributes(t, e.Path.ATTRIBUTE_NAMES); i && i(new e.Path(s.d, n(s, r))); }, void (e.Path.async = !0));
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = e.util.object.extend, r = e.util.array.invoke, n = e.Object.prototype.toObject;
    return e.PathGroup ? void e.warn("fabric.PathGroup is already defined") : (e.PathGroup = e.util.createClass(e.Path, { type: "path-group", fill: "",
        initialize: function (t, e) { e = e || {}, this.paths = t || []; for (var i = this.paths.length; i--;)
            this.paths[i].group = this; e.toBeParsed && (this.parseDimensionsFromPaths(e), delete e.toBeParsed), this.setOptions(e), this.setCoords(), e.sourcePath && this.setSourcePath(e.sourcePath); }, parseDimensionsFromPaths: function (t) { for (var i, r, n, s, o, a, h = [], c = [], l = this.paths.length; l--;) {
            n = this.paths[l], s = n.height + n.strokeWidth, o = n.width + n.strokeWidth, i = [{ x: n.left, y: n.top }, { x: n.left + o, y: n.top }, { x: n.left, y: n.top + s }, { x: n.left + o, y: n.top + s }], a = this.paths[l].transformMatrix;
            for (var u = 0; u < i.length; u++)
                r = i[u], a && (r = e.util.transformPoint(r, a, !1)), h.push(r.x), c.push(r.y);
        } t.width = Math.max.apply(null, h), t.height = Math.max.apply(null, c); }, render: function (t) { if (this.visible) {
            t.save(), this.transformMatrix && t.transform.apply(t, this.transformMatrix), this.transform(t), this._setShadow(t), this.clipTo && e.util.clipContext(this, t), t.translate(-this.width / 2, -this.height / 2);
            for (var i = 0, r = this.paths.length; i < r; ++i)
                this.paths[i].render(t, !0);
            this.clipTo && t.restore(), t.restore();
        } }, _set: function (t, e) { if ("fill" === t && e && this.isSameColor())
            for (var i = this.paths.length; i--;)
                this.paths[i]._set(t, e); return this.callSuper("_set", t, e); }, toObject: function (t) { var e = i(n.call(this, t), { paths: r(this.getObjects(), "toObject", t) }); return this.sourcePath && (e.sourcePath = this.sourcePath), e; }, toDatalessObject: function (t) { var e = this.toObject(t); return this.sourcePath && (e.paths = this.sourcePath), e; }, toSVG: function (t) { var e = this.getObjects(), i = this.getPointByOrigin("left", "top"), r = "translate(" + i.x + " " + i.y + ")", n = this._createBaseSVGMarkup(); n.push("<g ", this.getSvgId(), 'style="', this.getSvgStyles(), '" ', 'transform="', this.getSvgTransformMatrix(), r, this.getSvgTransform(), '" ', ">\n"); for (var s = 0, o = e.length; s < o; s++)
            n.push("\t", e[s].toSVG(t)); return n.push("</g>\n"), t ? t(n.join("")) : n.join(""); }, toString: function () { return "#<fabric.PathGroup (" + this.complexity() + "): { top: " + this.top + ", left: " + this.left + " }>"; }, isSameColor: function () { var t = this.getObjects()[0].get("fill") || ""; return "string" == typeof t && (t = t.toLowerCase(), this.getObjects().every(function (e) { var i = e.get("fill") || ""; return "string" == typeof i && i.toLowerCase() === t; })); }, complexity: function () { return this.paths.reduce(function (t, e) { return t + (e && e.complexity ? e.complexity() : 0); }, 0); }, getObjects: function () { return this.paths; } }), e.PathGroup.fromObject = function (t, i) { "string" == typeof t.paths ? e.loadSVGFromURL(t.paths, function (r) { var n = t.paths; delete t.paths; var s = e.util.groupSVGElements(r, t, n); i(s); }) : e.util.enlivenObjects(t.paths, function (r) { delete t.paths, i(new e.PathGroup(r, t)); }); }, void (e.PathGroup.async = !0));
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = e.util.object.extend, r = e.util.array.min, n = e.util.array.max, s = e.util.array.invoke;
    if (!e.Group) {
        var o = { lockMovementX: !0, lockMovementY: !0, lockRotation: !0, lockScalingX: !0, lockScalingY: !0, lockUniScaling: !0 };
        e.Group = e.util.createClass(e.Object, e.Collection, { type: "group", strokeWidth: 0, subTargetCheck: !1, initialize: function (t, e, i) { e = e || {}, this._objects = [], i && this.callSuper("initialize", e), this._objects = t || []; for (var r = this._objects.length; r--;)
                this._objects[r].group = this; this.originalState = {}, e.originX && (this.originX = e.originX), e.originY && (this.originY = e.originY), i ? this._updateObjectsCoords(!0) : (this._calcBounds(), this._updateObjectsCoords(), this.callSuper("initialize", e)), this.setCoords(), this.saveCoords(); }, _updateObjectsCoords: function (t) { for (var e = this._objects.length; e--;)
                this._updateObjectCoords(this._objects[e], t); }, _updateObjectCoords: function (t, e) { if (t.__origHasControls = t.hasControls, t.hasControls = !1, !e) {
                var i = t.getLeft(), r = t.getTop(), n = this.getCenterPoint();
                t.set({ originalLeft: i, originalTop: r, left: i - n.x, top: r - n.y }), t.setCoords();
            } }, toString: function () { return "#<fabric.Group: (" + this.complexity() + ")>"; }, addWithUpdate: function (t) { return this._restoreObjectsState(), e.util.resetObjectTransform(this), t && (this._objects.push(t), t.group = this, t._set("canvas", this.canvas)), this.forEachObject(this._setObjectActive, this), this._calcBounds(), this._updateObjectsCoords(), this; }, _setObjectActive: function (t) { t.set("active", !0), t.group = this; }, removeWithUpdate: function (t) { return this._restoreObjectsState(), e.util.resetObjectTransform(this), this.forEachObject(this._setObjectActive, this), this.remove(t), this._calcBounds(), this._updateObjectsCoords(), this; }, _onObjectAdded: function (t) { t.group = this, t._set("canvas", this.canvas); }, _onObjectRemoved: function (t) { delete t.group, t.set("active", !1); }, delegatedProperties: { fill: !0, stroke: !0, strokeWidth: !0, fontFamily: !0, fontWeight: !0, fontSize: !0, fontStyle: !0, lineHeight: !0, textDecoration: !0, textAlign: !0, backgroundColor: !0 }, _set: function (t, e) { var i = this._objects.length; if (this.delegatedProperties[t] || "canvas" === t)
                for (; i--;)
                    this._objects[i].set(t, e);
            else
                for (; i--;)
                    this._objects[i].setOnGroup(t, e); this.callSuper("_set", t, e); }, toObject: function (t) { return i(this.callSuper("toObject", t), { objects: s(this._objects, "toObject", t) }); }, render: function (t) { if (this.visible) {
                t.save(), this.transformMatrix && t.transform.apply(t, this.transformMatrix), this.transform(t), this._setShadow(t), this.clipTo && e.util.clipContext(this, t), this._transformDone = !0;
                for (var i = 0, r = this._objects.length; i < r; i++)
                    this._renderObject(this._objects[i], t);
                this.clipTo && t.restore(), t.restore(), this._transformDone = !1;
            } }, _renderControls: function (t, e) { this.callSuper("_renderControls", t, e); for (var i = 0, r = this._objects.length; i < r; i++)
                this._objects[i]._renderControls(t); }, _renderObject: function (t, e) { if (t.visible) {
                var i = t.hasRotatingPoint;
                t.hasRotatingPoint = !1, t.render(e), t.hasRotatingPoint = i;
            } }, _restoreObjectsState: function () { return this._objects.forEach(this._restoreObjectState, this), this; }, realizeTransform: function (t) { var i = t.calcTransformMatrix(), r = e.util.qrDecompose(i), n = new e.Point(r.translateX, r.translateY); return t.scaleX = r.scaleX, t.scaleY = r.scaleY, t.skewX = r.skewX, t.skewY = r.skewY, t.angle = r.angle, t.flipX = !1, t.flipY = !1, t.setPositionByOrigin(n, "center", "center"), t; }, _restoreObjectState: function (t) { return this.realizeTransform(t), t.setCoords(), t.hasControls = t.__origHasControls, delete t.__origHasControls, t.set("active", !1), delete t.group, this; }, destroy: function () { return this._restoreObjectsState(); }, saveCoords: function () { return this._originalLeft = this.get("left"), this._originalTop = this.get("top"), this; }, hasMoved: function () { return this._originalLeft !== this.get("left") || this._originalTop !== this.get("top"); }, setObjectsCoords: function () { return this.forEachObject(function (t) { t.setCoords(); }), this; }, _calcBounds: function (t) { for (var e, i, r, n = [], s = [], o = ["tr", "br", "bl", "tl"], a = 0, h = this._objects.length, c = o.length; a < h; ++a)
                for (e = this._objects[a], e.setCoords(), r = 0; r < c; r++)
                    i = o[r], n.push(e.oCoords[i].x), s.push(e.oCoords[i].y); this.set(this._getBounds(n, s, t)); }, _getBounds: function (t, i, s) { var o = e.util.invertTransform(this.getViewportTransform()), a = e.util.transformPoint(new e.Point(r(t), r(i)), o), h = e.util.transformPoint(new e.Point(n(t), n(i)), o), c = { width: h.x - a.x || 0, height: h.y - a.y || 0 }; return s || (c.left = a.x || 0, c.top = a.y || 0, "center" === this.originX && (c.left += c.width / 2), "right" === this.originX && (c.left += c.width), "center" === this.originY && (c.top += c.height / 2), "bottom" === this.originY && (c.top += c.height)), c; }, toSVG: function (t) { var e = this._createBaseSVGMarkup(); e.push("<g ", this.getSvgId(), 'transform="', this.getSvgTransform(), this.getSvgTransformMatrix(), '" style="', this.getSvgFilter(), '">\n'); for (var i = 0, r = this._objects.length; i < r; i++)
                e.push("\t", this._objects[i].toSVG(t)); return e.push("</g>\n"), t ? t(e.join("")) : e.join(""); }, get: function (t) { if (t in o) {
                if (this[t])
                    return this[t];
                for (var e = 0, i = this._objects.length; e < i; e++)
                    if (this._objects[e][t])
                        return !0;
                return !1;
            } return t in this.delegatedProperties ? this._objects[0] && this._objects[0].get(t) : this[t]; } }), e.Group.fromObject = function (t, i) { e.util.enlivenObjects(t.objects, function (r) { delete t.objects, i && i(new e.Group(r, t, !0)); }); }, e.Group.async = !0;
    }
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = fabric.util.object.extend;
    if (t.fabric || (t.fabric = {}), t.fabric.Image)
        return void fabric.warn("fabric.Image is already defined.");
    var i = fabric.Object.prototype.stateProperties.concat();
    i.push("alignX", "alignY", "meetOrSlice"), fabric.Image = fabric.util.createClass(fabric.Object, { type: "image", crossOrigin: "", alignX: "none", alignY: "none", meetOrSlice: "meet", strokeWidth: 0, _lastScaleX: 1, _lastScaleY: 1, minimumScaleTrigger: .5, stateProperties: i, initialize: function (t, e, i) { e || (e = {}), this.filters = [], this.resizeFilters = [], this.callSuper("initialize", e), this._initElement(t, e, i); }, getElement: function () { return this._element; }, setElement: function (t, e, i) { var r, n; return this._element = t, this._originalElement = t, this._initConfig(i), 0 === this.resizeFilters.length ? r = e : (n = this, r = function () { n.applyFilters(e, n.resizeFilters, n._filteredEl || n._originalElement, !0); }), 0 !== this.filters.length ? this.applyFilters(r) : r && r(this), this; }, setCrossOrigin: function (t) { return this.crossOrigin = t, this._element.crossOrigin = t, this; }, getOriginalSize: function () { var t = this.getElement(); return { width: t.width, height: t.height }; }, _stroke: function (t) { if (this.stroke && 0 !== this.strokeWidth) {
            var e = this.width / 2, i = this.height / 2;
            t.beginPath(), t.moveTo(-e, -i), t.lineTo(e, -i), t.lineTo(e, i), t.lineTo(-e, i), t.lineTo(-e, -i), t.closePath();
        } }, _renderDashedStroke: function (t) { var e = -this.width / 2, i = -this.height / 2, r = this.width, n = this.height; t.save(), this._setStrokeStyles(t), t.beginPath(), fabric.util.drawDashedLine(t, e, i, e + r, i, this.strokeDashArray), fabric.util.drawDashedLine(t, e + r, i, e + r, i + n, this.strokeDashArray), fabric.util.drawDashedLine(t, e + r, i + n, e, i + n, this.strokeDashArray), fabric.util.drawDashedLine(t, e, i + n, e, i, this.strokeDashArray), t.closePath(), t.restore(); }, toObject: function (t) { var i = [], r = [], n = 1, s = 1; this.filters.forEach(function (t) { t && ("Resize" === t.type && (n *= t.scaleX, s *= t.scaleY), i.push(t.toObject())); }), this.resizeFilters.forEach(function (t) { t && r.push(t.toObject()); }); var o = e(this.callSuper("toObject", t), { src: this.getSrc(), filters: i, resizeFilters: r, crossOrigin: this.crossOrigin, alignX: this.alignX, alignY: this.alignY, meetOrSlice: this.meetOrSlice }); return o.width /= n, o.height /= s, this.includeDefaultValues || this._removeDefaultValues(o), o; }, toSVG: function (t) { var e = this._createBaseSVGMarkup(), i = -this.width / 2, r = -this.height / 2, n = "none", s = !0; if (this.group && "path-group" === this.group.type && (i = this.left, r = this.top), "none" !== this.alignX && "none" !== this.alignY && (n = "x" + this.alignX + "Y" + this.alignY + " " + this.meetOrSlice), e.push('<g transform="', this.getSvgTransform(), this.getSvgTransformMatrix(), '">\n', "<image ", this.getSvgId(), 'xlink:href="', this.getSvgSrc(s), '" x="', i, '" y="', r, '" style="', this.getSvgStyles(), '" width="', this.width, '" height="', this.height, '" preserveAspectRatio="', n, '"', "></image>\n"), this.stroke || this.strokeDashArray) {
            var o = this.fill;
            this.fill = null, e.push("<rect ", 'x="', i, '" y="', r, '" width="', this.width, '" height="', this.height, '" style="', this.getSvgStyles(), '"/>\n'), this.fill = o;
        } return e.push("</g>\n"), t ? t(e.join("")) : e.join(""); }, getSrc: function (t) { var e = t ? this._element : this._originalElement; return e ? fabric.isLikelyNode ? e._src : e.src : this.src || ""; }, setSrc: function (t, e, i) { fabric.util.loadImage(t, function (t) { return this.setElement(t, e, i); }, this, i && i.crossOrigin); }, toString: function () { return '#<fabric.Image: { src: "' + this.getSrc() + '" }>'; }, applyFilters: function (t, e, i, r) { if (e = e || this.filters, i = i || this._originalElement) {
            var n, s, o = fabric.util.createImage(), a = this.canvas ? this.canvas.getRetinaScaling() : fabric.devicePixelRatio, h = this.minimumScaleTrigger / a, c = this;
            if (0 === e.length)
                return this._element = i, t && t(this), i;
            var l = fabric.util.createCanvasElement();
            return l.width = i.width, l.height = i.height, l.getContext("2d").drawImage(i, 0, 0, i.width, i.height), e.forEach(function (t) { t && (r ? (n = c.scaleX < h ? c.scaleX : 1, s = c.scaleY < h ? c.scaleY : 1, n * a < 1 && (n *= a), s * a < 1 && (s *= a)) : (n = t.scaleX, s = t.scaleY), t.applyTo(l, n, s), r || "Resize" !== t.type || (c.width *= t.scaleX, c.height *= t.scaleY)); }), o.width = l.width, o.height = l.height, fabric.isLikelyNode ? (o.src = l.toBuffer(void 0, fabric.Image.pngCompression), c._element = o, !r && (c._filteredEl = o), t && t(c)) : (o.onload = function () { c._element = o, !r && (c._filteredEl = o), t && t(c), o.onload = l = null; }, o.src = l.toDataURL("image/png")), l;
        } }, _render: function (t, e) { var i, r, n, s = this._findMargins(); i = e ? this.left : -this.width / 2, r = e ? this.top : -this.height / 2, "slice" === this.meetOrSlice && (t.beginPath(), t.rect(i, r, this.width, this.height), t.clip()), this.isMoving === !1 && this.resizeFilters.length && this._needsResize() ? (this._lastScaleX = this.scaleX, this._lastScaleY = this.scaleY, n = this.applyFilters(null, this.resizeFilters, this._filteredEl || this._originalElement, !0)) : n = this._element, n && t.drawImage(n, i + s.marginX, r + s.marginY, s.width, s.height), this._stroke(t), this._renderStroke(t); }, _needsResize: function () { return this.scaleX !== this._lastScaleX || this.scaleY !== this._lastScaleY; }, _findMargins: function () { var t, e, i = this.width, r = this.height, n = 0, s = 0; return "none" === this.alignX && "none" === this.alignY || (t = [this.width / this._element.width, this.height / this._element.height], e = "meet" === this.meetOrSlice ? Math.min.apply(null, t) : Math.max.apply(null, t), i = this._element.width * e, r = this._element.height * e, "Mid" === this.alignX && (n = (this.width - i) / 2), "Max" === this.alignX && (n = this.width - i), "Mid" === this.alignY && (s = (this.height - r) / 2), "Max" === this.alignY && (s = this.height - r)), { width: i, height: r, marginX: n, marginY: s }; }, _resetWidthHeight: function () { var t = this.getElement(); this.set("width", t.width), this.set("height", t.height); }, _initElement: function (t, e, i) { this.setElement(fabric.util.getById(t), i, e), fabric.util.addClass(this.getElement(), fabric.Image.CSS_CANVAS); }, _initConfig: function (t) { t || (t = {}), this.setOptions(t), this._setWidthHeight(t), this._element && this.crossOrigin && (this._element.crossOrigin = this.crossOrigin); }, _initFilters: function (t, e) { t && t.length ? fabric.util.enlivenObjects(t, function (t) { e && e(t); }, "fabric.Image.filters") : e && e(); }, _setWidthHeight: function (t) { this.width = "width" in t ? t.width : this.getElement() ? this.getElement().width || 0 : 0, this.height = "height" in t ? t.height : this.getElement() ? this.getElement().height || 0 : 0; }, complexity: function () { return 1; } }), fabric.Image.CSS_CANVAS = "canvas-img", fabric.Image.prototype.getSvgSrc = fabric.Image.prototype.getSrc, fabric.Image.fromObject = function (t, e) { fabric.util.loadImage(t.src, function (i) { fabric.Image.prototype._initFilters.call(t, t.filters, function (r) { t.filters = r || [], fabric.Image.prototype._initFilters.call(t, t.resizeFilters, function (r) { return t.resizeFilters = r || [], new fabric.Image(i, t, e); }); }); }, null, t.crossOrigin); }, fabric.Image.fromURL = function (t, e, i) { fabric.util.loadImage(t, function (t) { e && e(new fabric.Image(t, i)); }, null, i && i.crossOrigin); }, fabric.Image.ATTRIBUTE_NAMES = fabric.SHARED_ATTRIBUTES.concat("x y width height preserveAspectRatio xlink:href".split(" ")), fabric.Image.fromElement = function (t, i, r) { var n, s = fabric.parseAttributes(t, fabric.Image.ATTRIBUTE_NAMES); s.preserveAspectRatio && (n = fabric.util.parsePreserveAspectRatioAttribute(s.preserveAspectRatio), e(s, n)), fabric.Image.fromURL(s["xlink:href"], i, e(r ? fabric.util.object.clone(r) : {}, s)); }, fabric.Image.async = !0, fabric.Image.pngCompression = 1;
}("undefined" != typeof exports ? exports : this), fabric.util.object.extend(fabric.Object.prototype, { _getAngleValueForStraighten: function () { var t = this.getAngle() % 360; return t > 0 ? 90 * Math.round((t - 1) / 90) : 90 * Math.round(t / 90); }, straighten: function () { return this.setAngle(this._getAngleValueForStraighten()), this; }, fxStraighten: function (t) { t = t || {}; var e = function () { }, i = t.onComplete || e, r = t.onChange || e, n = this; return fabric.util.animate({ startValue: this.get("angle"), endValue: this._getAngleValueForStraighten(), duration: this.FX_DURATION, onChange: function (t) { n.setAngle(t), r(); }, onComplete: function () { n.setCoords(), i(); }, onStart: function () { n.set("active", !1); } }), this; } }), fabric.util.object.extend(fabric.StaticCanvas.prototype, { straightenObject: function (t) { return t.straighten(), this.renderAll(), this; }, fxStraightenObject: function (t) { return t.fxStraighten({ onChange: this.renderAll.bind(this) }), this; } }), fabric.Image.filters = fabric.Image.filters || {}, fabric.Image.filters.BaseFilter = fabric.util.createClass({ type: "BaseFilter", initialize: function (t) { t && this.setOptions(t); }, setOptions: function (t) { for (var e in t)
        this[e] = t[e]; }, toObject: function () { return { type: this.type }; }, toJSON: function () { return this.toObject(); } }), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = e.util.object.extend, r = e.Image.filters, n = e.util.createClass;
    r.Brightness = n(r.BaseFilter, { type: "Brightness", initialize: function (t) { t = t || {}, this.brightness = t.brightness || 0; }, applyTo: function (t) { for (var e = t.getContext("2d"), i = e.getImageData(0, 0, t.width, t.height), r = i.data, n = this.brightness, s = 0, o = r.length; s < o; s += 4)
            r[s] += n, r[s + 1] += n, r[s + 2] += n; e.putImageData(i, 0, 0); }, toObject: function () { return i(this.callSuper("toObject"), { brightness: this.brightness }); } }), e.Image.filters.Brightness.fromObject = function (t) { return new e.Image.filters.Brightness(t); };
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = e.util.object.extend, r = e.Image.filters, n = e.util.createClass;
    r.Convolute = n(r.BaseFilter, { type: "Convolute", initialize: function (t) { t = t || {}, this.opaque = t.opaque, this.matrix = t.matrix || [0, 0, 0, 0, 1, 0, 0, 0, 0]; }, applyTo: function (t) { for (var e, i, r, n, s, o, a, h, c, l = this.matrix, u = t.getContext("2d"), f = u.getImageData(0, 0, t.width, t.height), d = Math.round(Math.sqrt(l.length)), g = Math.floor(d / 2), p = f.data, v = f.width, b = f.height, m = u.createImageData(v, b), y = m.data, _ = this.opaque ? 1 : 0, x = 0; x < b; x++)
            for (var S = 0; S < v; S++) {
                s = 4 * (x * v + S), e = 0, i = 0, r = 0, n = 0;
                for (var C = 0; C < d; C++)
                    for (var w = 0; w < d; w++)
                        a = x + C - g, o = S + w - g, a < 0 || a > b || o < 0 || o > v || (h = 4 * (a * v + o), c = l[C * d + w], e += p[h] * c, i += p[h + 1] * c, r += p[h + 2] * c, n += p[h + 3] * c);
                y[s] = e, y[s + 1] = i, y[s + 2] = r, y[s + 3] = n + _ * (255 - n);
            } u.putImageData(m, 0, 0); }, toObject: function () { return i(this.callSuper("toObject"), { opaque: this.opaque, matrix: this.matrix }); } }), e.Image.filters.Convolute.fromObject = function (t) { return new e.Image.filters.Convolute(t); };
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = e.util.object.extend, r = e.Image.filters, n = e.util.createClass;
    r.GradientTransparency = n(r.BaseFilter, { type: "GradientTransparency", initialize: function (t) { t = t || {}, this.threshold = t.threshold || 100; }, applyTo: function (t) { for (var e = t.getContext("2d"), i = e.getImageData(0, 0, t.width, t.height), r = i.data, n = this.threshold, s = r.length, o = 0, a = r.length; o < a; o += 4)
            r[o + 3] = n + 255 * (s - o) / s; e.putImageData(i, 0, 0); }, toObject: function () { return i(this.callSuper("toObject"), { threshold: this.threshold }); } }), e.Image.filters.GradientTransparency.fromObject = function (t) { return new e.Image.filters.GradientTransparency(t); };
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = e.Image.filters, r = e.util.createClass;
    i.Grayscale = r(i.BaseFilter, { type: "Grayscale", applyTo: function (t) { for (var e, i = t.getContext("2d"), r = i.getImageData(0, 0, t.width, t.height), n = r.data, s = r.width * r.height * 4, o = 0; o < s;)
            e = (n[o] + n[o + 1] + n[o + 2]) / 3, n[o] = e, n[o + 1] = e, n[o + 2] = e, o += 4; i.putImageData(r, 0, 0); } }), e.Image.filters.Grayscale.fromObject = function () { return new e.Image.filters.Grayscale; };
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = e.Image.filters, r = e.util.createClass;
    i.Invert = r(i.BaseFilter, { type: "Invert", applyTo: function (t) { var e, i = t.getContext("2d"), r = i.getImageData(0, 0, t.width, t.height), n = r.data, s = n.length; for (e = 0; e < s; e += 4)
            n[e] = 255 - n[e], n[e + 1] = 255 - n[e + 1], n[e + 2] = 255 - n[e + 2]; i.putImageData(r, 0, 0); } }), e.Image.filters.Invert.fromObject = function () { return new e.Image.filters.Invert; };
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = e.util.object.extend, r = e.Image.filters, n = e.util.createClass;
    r.Mask = n(r.BaseFilter, { type: "Mask", initialize: function (t) { t = t || {}, this.mask = t.mask, this.channel = [0, 1, 2, 3].indexOf(t.channel) > -1 ? t.channel : 0; }, applyTo: function (t) { if (this.mask) {
            var i, r = t.getContext("2d"), n = r.getImageData(0, 0, t.width, t.height), s = n.data, o = this.mask.getElement(), a = e.util.createCanvasElement(), h = this.channel, c = n.width * n.height * 4;
            a.width = t.width, a.height = t.height, a.getContext("2d").drawImage(o, 0, 0, t.width, t.height);
            var l = a.getContext("2d").getImageData(0, 0, t.width, t.height), u = l.data;
            for (i = 0; i < c; i += 4)
                s[i + 3] = u[i + h];
            r.putImageData(n, 0, 0);
        } }, toObject: function () { return i(this.callSuper("toObject"), { mask: this.mask.toObject(), channel: this.channel }); } }), e.Image.filters.Mask.fromObject = function (t, i) { e.util.loadImage(t.mask.src, function (r) { t.mask = new e.Image(r, t.mask), i && i(new e.Image.filters.Mask(t)); }); }, e.Image.filters.Mask.async = !0;
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = e.util.object.extend, r = e.Image.filters, n = e.util.createClass;
    r.Noise = n(r.BaseFilter, { type: "Noise", initialize: function (t) { t = t || {}, this.noise = t.noise || 0; }, applyTo: function (t) { for (var e, i = t.getContext("2d"), r = i.getImageData(0, 0, t.width, t.height), n = r.data, s = this.noise, o = 0, a = n.length; o < a; o += 4)
            e = (.5 - Math.random()) * s, n[o] += e, n[o + 1] += e, n[o + 2] += e; i.putImageData(r, 0, 0); }, toObject: function () { return i(this.callSuper("toObject"), { noise: this.noise }); } }), e.Image.filters.Noise.fromObject = function (t) { return new e.Image.filters.Noise(t); };
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = e.util.object.extend, r = e.Image.filters, n = e.util.createClass;
    r.Pixelate = n(r.BaseFilter, { type: "Pixelate", initialize: function (t) { t = t || {}, this.blocksize = t.blocksize || 4; }, applyTo: function (t) { var e, i, r, n, s, o, a, h = t.getContext("2d"), c = h.getImageData(0, 0, t.width, t.height), l = c.data, u = c.height, f = c.width; for (i = 0; i < u; i += this.blocksize)
            for (r = 0; r < f; r += this.blocksize) {
                e = 4 * i * f + 4 * r, n = l[e], s = l[e + 1], o = l[e + 2], a = l[e + 3];
                for (var d = i, g = i + this.blocksize; d < g; d++)
                    for (var p = r, v = r + this.blocksize; p < v; p++)
                        e = 4 * d * f + 4 * p, l[e] = n, l[e + 1] = s, l[e + 2] = o, l[e + 3] = a;
            } h.putImageData(c, 0, 0); }, toObject: function () { return i(this.callSuper("toObject"), { blocksize: this.blocksize }); } }), e.Image.filters.Pixelate.fromObject = function (t) { return new e.Image.filters.Pixelate(t); };
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = e.util.object.extend, r = e.Image.filters, n = e.util.createClass;
    r.RemoveWhite = n(r.BaseFilter, { type: "RemoveWhite", initialize: function (t) { t = t || {}, this.threshold = t.threshold || 30, this.distance = t.distance || 20; }, applyTo: function (t) { for (var e, i, r, n = t.getContext("2d"), s = n.getImageData(0, 0, t.width, t.height), o = s.data, a = this.threshold, h = this.distance, c = 255 - a, l = Math.abs, u = 0, f = o.length; u < f; u += 4)
            e = o[u], i = o[u + 1], r = o[u + 2], e > c && i > c && r > c && l(e - i) < h && l(e - r) < h && l(i - r) < h && (o[u + 3] = 0); n.putImageData(s, 0, 0); }, toObject: function () { return i(this.callSuper("toObject"), { threshold: this.threshold, distance: this.distance }); } }), e.Image.filters.RemoveWhite.fromObject = function (t) { return new e.Image.filters.RemoveWhite(t); };
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = e.Image.filters, r = e.util.createClass;
    i.Sepia = r(i.BaseFilter, { type: "Sepia", applyTo: function (t) { var e, i, r = t.getContext("2d"), n = r.getImageData(0, 0, t.width, t.height), s = n.data, o = s.length; for (e = 0; e < o; e += 4)
            i = .3 * s[e] + .59 * s[e + 1] + .11 * s[e + 2], s[e] = i + 100, s[e + 1] = i + 50, s[e + 2] = i + 255; r.putImageData(n, 0, 0); } }), e.Image.filters.Sepia.fromObject = function () { return new e.Image.filters.Sepia; };
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = e.Image.filters, r = e.util.createClass;
    i.Sepia2 = r(i.BaseFilter, { type: "Sepia2", applyTo: function (t) { var e, i, r, n, s = t.getContext("2d"), o = s.getImageData(0, 0, t.width, t.height), a = o.data, h = a.length; for (e = 0; e < h; e += 4)
            i = a[e], r = a[e + 1], n = a[e + 2], a[e] = (.393 * i + .769 * r + .189 * n) / 1.351, a[e + 1] = (.349 * i + .686 * r + .168 * n) / 1.203, a[e + 2] = (.272 * i + .534 * r + .131 * n) / 2.14; s.putImageData(o, 0, 0); } }), e.Image.filters.Sepia2.fromObject = function () { return new e.Image.filters.Sepia2; };
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = e.util.object.extend, r = e.Image.filters, n = e.util.createClass;
    r.Tint = n(r.BaseFilter, { type: "Tint", initialize: function (t) { t = t || {}, this.color = t.color || "#000000", this.opacity = "undefined" != typeof t.opacity ? t.opacity : new e.Color(this.color).getAlpha(); }, applyTo: function (t) { var i, r, n, s, o, a, h, c, l, u = t.getContext("2d"), f = u.getImageData(0, 0, t.width, t.height), d = f.data, g = d.length; for (l = new e.Color(this.color).getSource(), r = l[0] * this.opacity, n = l[1] * this.opacity, s = l[2] * this.opacity, c = 1 - this.opacity, i = 0; i < g; i += 4)
            o = d[i], a = d[i + 1], h = d[i + 2], d[i] = r + o * c, d[i + 1] = n + a * c, d[i + 2] = s + h * c; u.putImageData(f, 0, 0); }, toObject: function () { return i(this.callSuper("toObject"), { color: this.color, opacity: this.opacity }); } }), e.Image.filters.Tint.fromObject = function (t) { return new e.Image.filters.Tint(t); };
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = e.util.object.extend, r = e.Image.filters, n = e.util.createClass;
    r.Multiply = n(r.BaseFilter, { type: "Multiply", initialize: function (t) { t = t || {}, this.color = t.color || "#000000"; }, applyTo: function (t) { var i, r, n = t.getContext("2d"), s = n.getImageData(0, 0, t.width, t.height), o = s.data, a = o.length; for (r = new e.Color(this.color).getSource(), i = 0; i < a; i += 4)
            o[i] *= r[0] / 255, o[i + 1] *= r[1] / 255, o[i + 2] *= r[2] / 255; n.putImageData(s, 0, 0); }, toObject: function () { return i(this.callSuper("toObject"), { color: this.color }); } }), e.Image.filters.Multiply.fromObject = function (t) { return new e.Image.filters.Multiply(t); };
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric, i = e.Image.filters, r = e.util.createClass;
    i.Blend = r(i.BaseFilter, { type: "Blend", initialize: function (t) { t = t || {}, this.color = t.color || "#000", this.image = t.image || !1, this.mode = t.mode || "multiply", this.alpha = t.alpha || 1; }, applyTo: function (t) { var i, r, n, s, o, a, h, c, l, u, f = t.getContext("2d"), d = f.getImageData(0, 0, t.width, t.height), g = d.data, p = !1; if (this.image) {
            p = !0;
            var v = e.util.createCanvasElement();
            v.width = this.image.width, v.height = this.image.height;
            var b = new e.StaticCanvas(v);
            b.add(this.image);
            var m = b.getContext("2d");
            u = m.getImageData(0, 0, b.width, b.height).data;
        }
        else
            u = new e.Color(this.color).getSource(), i = u[0] * this.alpha, r = u[1] * this.alpha, n = u[2] * this.alpha; for (var y = 0, _ = g.length; y < _; y += 4)
            switch (s = g[y], o = g[y + 1], a = g[y + 2], p && (i = u[y] * this.alpha, r = u[y + 1] * this.alpha, n = u[y + 2] * this.alpha), this.mode) {
                case "multiply":
                    g[y] = s * i / 255, g[y + 1] = o * r / 255, g[y + 2] = a * n / 255;
                    break;
                case "screen":
                    g[y] = 1 - (1 - s) * (1 - i), g[y + 1] = 1 - (1 - o) * (1 - r), g[y + 2] = 1 - (1 - a) * (1 - n);
                    break;
                case "add":
                    g[y] = Math.min(255, s + i), g[y + 1] = Math.min(255, o + r), g[y + 2] = Math.min(255, a + n);
                    break;
                case "diff":
                case "difference":
                    g[y] = Math.abs(s - i), g[y + 1] = Math.abs(o - r), g[y + 2] = Math.abs(a - n);
                    break;
                case "subtract":
                    h = s - i, c = o - r, l = a - n, g[y] = h < 0 ? 0 : h, g[y + 1] = c < 0 ? 0 : c, g[y + 2] = l < 0 ? 0 : l;
                    break;
                case "darken":
                    g[y] = Math.min(s, i), g[y + 1] = Math.min(o, r), g[y + 2] = Math.min(a, n);
                    break;
                case "lighten": g[y] = Math.max(s, i), g[y + 1] = Math.max(o, r), g[y + 2] = Math.max(a, n);
            } f.putImageData(d, 0, 0); }, toObject: function () { return { color: this.color, image: this.image, mode: this.mode, alpha: this.alpha }; } }), e.Image.filters.Blend.fromObject = function (t) { return new e.Image.filters.Blend(t); };
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = Math.pow, r = Math.floor, n = Math.sqrt, s = Math.abs, o = Math.max, a = Math.round, h = Math.sin, c = Math.ceil, l = e.Image.filters, u = e.util.createClass;
    l.Resize = u(l.BaseFilter, { type: "Resize", resizeType: "hermite", scaleX: 0, scaleY: 0, lanczosLobes: 3, applyTo: function (t, e, i) { if (1 !== e || 1 !== i) {
            this.rcpScaleX = 1 / e, this.rcpScaleY = 1 / i;
            var r, n = t.width, s = t.height, o = a(n * e), h = a(s * i);
            "sliceHack" === this.resizeType && (r = this.sliceByTwo(t, n, s, o, h)), "hermite" === this.resizeType && (r = this.hermiteFastResize(t, n, s, o, h)), "bilinear" === this.resizeType && (r = this.bilinearFiltering(t, n, s, o, h)), "lanczos" === this.resizeType && (r = this.lanczosResize(t, n, s, o, h)), t.width = o, t.height = h, t.getContext("2d").putImageData(r, 0, 0);
        } }, sliceByTwo: function (t, i, n, s, a) { var h, c = t.getContext("2d"), l = .5, u = .5, f = 1, d = 1, g = !1, p = !1, v = i, b = n, m = e.util.createCanvasElement(), y = m.getContext("2d"); for (s = r(s), a = r(a), m.width = o(s, i), m.height = o(a, n), s > i && (l = 2, f = -1), a > n && (u = 2, d = -1), h = c.getImageData(0, 0, i, n), t.width = o(s, i), t.height = o(a, n), c.putImageData(h, 0, 0); !g || !p;)
            i = v, n = b, s * f < r(v * l * f) ? v = r(v * l) : (v = s, g = !0), a * d < r(b * u * d) ? b = r(b * u) : (b = a, p = !0), h = c.getImageData(0, 0, i, n), y.putImageData(h, 0, 0), c.clearRect(0, 0, v, b), c.drawImage(m, 0, 0, i, n, 0, 0, v, b); return c.getImageData(0, 0, s, a); }, lanczosResize: function (t, e, o, a, l) { function u(t) { return function (e) { if (e > t)
            return 0; if (e *= Math.PI, s(e) < 1e-16)
            return 1; var i = e / t; return h(e) * h(i) / e / i; }; } function f(t) { var h, c, u, d, g, j, M, A, P, I, D; for (T.x = (t + .5) * y, k.x = r(T.x), h = 0; h < l; h++) {
            for (T.y = (h + .5) * _, k.y = r(T.y), g = 0, j = 0, M = 0, A = 0, P = 0, c = k.x - C; c <= k.x + C; c++)
                if (!(c < 0 || c >= e)) {
                    I = r(1e3 * s(c - T.x)), O[I] || (O[I] = {});
                    for (var E = k.y - w; E <= k.y + w; E++)
                        E < 0 || E >= o || (D = r(1e3 * s(E - T.y)), O[I][D] || (O[I][D] = m(n(i(I * x, 2) + i(D * S, 2)) / 1e3)), u = O[I][D], u > 0 && (d = 4 * (E * e + c), g += u, j += u * v[d], M += u * v[d + 1], A += u * v[d + 2], P += u * v[d + 3]));
                }
            d = 4 * (h * a + t), b[d] = j / g, b[d + 1] = M / g, b[d + 2] = A / g, b[d + 3] = P / g;
        } return ++t < a ? f(t) : p; } var d = t.getContext("2d"), g = d.getImageData(0, 0, e, o), p = d.getImageData(0, 0, a, l), v = g.data, b = p.data, m = u(this.lanczosLobes), y = this.rcpScaleX, _ = this.rcpScaleY, x = 2 / this.rcpScaleX, S = 2 / this.rcpScaleY, C = c(y * this.lanczosLobes / 2), w = c(_ * this.lanczosLobes / 2), O = {}, T = {}, k = {}; return f(0); }, bilinearFiltering: function (t, e, i, n, s) { var o, a, h, c, l, u, f, d, g, p, v, b, m, y = 0, _ = this.rcpScaleX, x = this.rcpScaleY, S = t.getContext("2d"), C = 4 * (e - 1), w = S.getImageData(0, 0, e, i), O = w.data, T = S.getImageData(0, 0, n, s), k = T.data; for (f = 0; f < s; f++)
            for (d = 0; d < n; d++)
                for (l = r(_ * d), u = r(x * f), g = _ * d - l, p = x * f - u, m = 4 * (u * e + l), v = 0; v < 4; v++)
                    o = O[m + v], a = O[m + 4 + v], h = O[m + C + v], c = O[m + C + 4 + v], b = o * (1 - g) * (1 - p) + a * g * (1 - p) + h * p * (1 - g) + c * g * p, k[y++] = b; return T; }, hermiteFastResize: function (t, e, i, o, a) { for (var h = this.rcpScaleX, l = this.rcpScaleY, u = c(h / 2), f = c(l / 2), d = t.getContext("2d"), g = d.getImageData(0, 0, e, i), p = g.data, v = d.getImageData(0, 0, o, a), b = v.data, m = 0; m < a; m++)
            for (var y = 0; y < o; y++) {
                for (var _ = 4 * (y + m * o), x = 0, S = 0, C = 0, w = 0, O = 0, T = 0, k = 0, j = (m + .5) * l, M = r(m * l); M < (m + 1) * l; M++)
                    for (var A = s(j - (M + .5)) / f, P = (y + .5) * h, I = A * A, D = r(y * h); D < (y + 1) * h; D++) {
                        var E = s(P - (D + .5)) / u, L = n(I + E * E);
                        L > 1 && L < -1 || (x = 2 * L * L * L - 3 * L * L + 1, x > 0 && (E = 4 * (D + M * e), k += x * p[E + 3], C += x, p[E + 3] < 255 && (x = x * p[E + 3] / 250), w += x * p[E], O += x * p[E + 1], T += x * p[E + 2], S += x));
                    }
                b[_] = w / S, b[_ + 1] = O / S, b[_ + 2] = T / S, b[_ + 3] = k / C;
            } return v; }, toObject: function () { return { type: this.type, scaleX: this.scaleX, scaleY: this.scaleY, resizeType: this.resizeType, lanczosLobes: this.lanczosLobes }; } }), e.Image.filters.Resize.fromObject = function (t) { return new e.Image.filters.Resize(t); };
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = e.util.object.extend, r = e.Image.filters, n = e.util.createClass;
    r.ColorMatrix = n(r.BaseFilter, { type: "ColorMatrix", initialize: function (t) { t || (t = {}), this.matrix = t.matrix || [1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0]; }, applyTo: function (t) { var e, i, r, n, s, o = t.getContext("2d"), a = o.getImageData(0, 0, t.width, t.height), h = a.data, c = h.length, l = this.matrix; for (e = 0; e < c; e += 4)
            i = h[e], r = h[e + 1], n = h[e + 2], s = h[e + 3], h[e] = i * l[0] + r * l[1] + n * l[2] + s * l[3] + l[4], h[e + 1] = i * l[5] + r * l[6] + n * l[7] + s * l[8] + l[9], h[e + 2] = i * l[10] + r * l[11] + n * l[12] + s * l[13] + l[14], h[e + 3] = i * l[15] + r * l[16] + n * l[17] + s * l[18] + l[19]; o.putImageData(a, 0, 0); }, toObject: function () { return i(this.callSuper("toObject"), { type: this.type, matrix: this.matrix }); } }), e.Image.filters.ColorMatrix.fromObject = function (t) { return new e.Image.filters.ColorMatrix(t); };
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = e.util.object.extend, r = e.Image.filters, n = e.util.createClass;
    r.Contrast = n(r.BaseFilter, { type: "Contrast", initialize: function (t) { t = t || {}, this.contrast = t.contrast || 0; }, applyTo: function (t) { for (var e = t.getContext("2d"), i = e.getImageData(0, 0, t.width, t.height), r = i.data, n = 259 * (this.contrast + 255) / (255 * (259 - this.contrast)), s = 0, o = r.length; s < o; s += 4)
            r[s] = n * (r[s] - 128) + 128, r[s + 1] = n * (r[s + 1] - 128) + 128, r[s + 2] = n * (r[s + 2] - 128) + 128; e.putImageData(i, 0, 0); }, toObject: function () { return i(this.callSuper("toObject"), { contrast: this.contrast }); } }), e.Image.filters.Contrast.fromObject = function (t) { return new e.Image.filters.Contrast(t); };
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = e.util.object.extend, r = e.Image.filters, n = e.util.createClass;
    r.Saturate = n(r.BaseFilter, {
        type: "Saturate", initialize: function (t) { t = t || {}, this.saturate = t.saturate || 0, this.loadProgram(); }, applyTo: function (t) { for (var e, i = t.getContext("2d"), r = i.getImageData(0, 0, t.width, t.height), n = r.data, s = .01 * -this.saturate, o = 0, a = n.length; o < a; o += 4)
            e = Math.max(n[o], n[o + 1], n[o + 2]), n[o] += e !== n[o] ? (e - n[o]) * s : 0, n[o + 1] += e !== n[o + 1] ? (e - n[o + 1]) * s : 0, n[o + 2] += e !== n[o + 2] ? (e - n[o + 2]) * s : 0; i.putImageData(r, 0, 0); }, toObject: function () { return i(this.callSuper("toObject"), { saturate: this.saturate }); } }), e.Image.filters.Saturate.fromObject = function (t) { return new e.Image.filters.Saturate(t); };
}("undefined" != typeof exports ? exports : this), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = e.util.object.extend, r = e.util.object.clone, n = e.util.toFixed, s = e.Object.NUM_FRACTION_DIGITS, o = 2;
    if (e.Text)
        return void e.warn("fabric.Text is already defined");
    var a = e.Object.prototype.stateProperties.concat();
    a.push("fontFamily", "fontWeight", "fontSize", "text", "textDecoration", "textAlign", "fontStyle", "lineHeight", "textBackgroundColor"), e.Text = e.util.createClass(e.Object, { _dimensionAffectingProps: { fontSize: !0, fontWeight: !0, fontFamily: !0, fontStyle: !0, lineHeight: !0, text: !0, charSpacing: !0, textAlign: !0, strokeWidth: !1 }, _reNewline: /\r?\n/, _reSpacesAndTabs: /[ \t\r]+/g, type: "text", fontSize: 40, fontWeight: "normal", fontFamily: "Times New Roman", textDecoration: "", textAlign: "left", fontStyle: "", lineHeight: 1.16, textBackgroundColor: "", stateProperties: a, stroke: null, shadow: null, _fontSizeFraction: .25, _fontSizeMult: 1.13, charSpacing: 0, initialize: function (t, e) { e = e || {}, this.text = t, this.__skipDimension = !0, this.setOptions(e), this.__skipDimension = !1, this._initDimensions(); }, _initDimensions: function (t) { this.__skipDimension || (t || (t = e.util.createCanvasElement().getContext("2d"), this._setTextStyles(t)), this._textLines = this._splitTextIntoLines(), this._clearCache(), this.width = this._getTextWidth(t) || this.cursorWidth || o, this.height = this._getTextHeight(t)); }, toString: function () { return "#<fabric.Text (" + this.complexity() + '): { "text": "' + this.text + '", "fontFamily": "' + this.fontFamily + '" }>'; }, _render: function (t) { this.clipTo && e.util.clipContext(this, t), this._setOpacity(t), this._setShadow(t), this._setupCompositeOperation(t), this._renderTextBackground(t), this._setStrokeStyles(t), this._setFillStyles(t), this._renderText(t), this._renderTextDecoration(t), this.clipTo && t.restore(); }, _renderText: function (t) { this._renderTextFill(t), this._renderTextStroke(t); }, _setTextStyles: function (t) { t.textBaseline = "alphabetic", t.font = this._getFontDeclaration(); }, _getTextHeight: function () { return this._getHeightOfSingleLine() + (this._textLines.length - 1) * this._getHeightOfLine(); }, _getTextWidth: function (t) { for (var e = this._getLineWidth(t, 0), i = 1, r = this._textLines.length; i < r; i++) {
            var n = this._getLineWidth(t, i);
            n > e && (e = n);
        } return e; }, _getNonTransformedDimensions: function () { return { x: this.width, y: this.height }; }, _renderChars: function (t, e, i, r, n) { var s, o, a = t.slice(0, -4); if (this[a].toLive) {
            var h = -this.width / 2 + this[a].offsetX || 0, c = -this.height / 2 + this[a].offsetY || 0;
            e.save(), e.translate(h, c), r -= h, n -= c;
        } if (0 !== this.charSpacing) {
            var l = this._getWidthOfCharSpacing();
            i = i.split("");
            for (var u = 0, f = i.length; u < f; u++)
                s = i[u], o = e.measureText(s).width + l, e[t](s, r, n), r += o > 0 ? o : 0;
        }
        else
            e[t](i, r, n); this[a].toLive && e.restore(); }, _renderTextLine: function (t, e, i, r, n, s) { n -= this.fontSize * this._fontSizeFraction; var o = this._getLineWidth(e, s); if ("justify" !== this.textAlign || this.width < o)
            return void this._renderChars(t, e, i, r, n, s); for (var a, h = i.split(/\s+/), c = 0, l = this._getWidthOfWords(e, h.join(""), s, 0), u = this.width - l, f = h.length - 1, d = f > 0 ? u / f : 0, g = 0, p = 0, v = h.length; p < v; p++) {
            for (; " " === i[c] && c < i.length;)
                c++;
            a = h[p], this._renderChars(t, e, a, r + g, n, s, c), g += this._getWidthOfWords(e, a, s, c) + d, c += a.length;
        } }, _getWidthOfWords: function (t, e) { var i, r, n = t.measureText(e).width; return 0 !== this.charSpacing && (i = e.split("").length, r = i * this._getWidthOfCharSpacing(), n += r), n > 0 ? n : 0; }, _getLeftOffset: function () { return -this.width / 2; }, _getTopOffset: function () { return -this.height / 2; }, isEmptyStyles: function () { return !0; }, _renderTextCommon: function (t, e) { for (var i = 0, r = this._getLeftOffset(), n = this._getTopOffset(), s = 0, o = this._textLines.length; s < o; s++) {
            var a = this._getHeightOfLine(t, s), h = a / this.lineHeight, c = this._getLineWidth(t, s), l = this._getLineLeftOffset(c);
            this._renderTextLine(e, t, this._textLines[s], r + l, n + i + h, s), i += a;
        } }, _renderTextFill: function (t) { !this.fill && this.isEmptyStyles() || this._renderTextCommon(t, "fillText"); }, _renderTextStroke: function (t) { (this.stroke && 0 !== this.strokeWidth || !this.isEmptyStyles()) && (this.shadow && !this.shadow.affectStroke && this._removeShadow(t), t.save(), this._setLineDash(t, this.strokedashArray), t.beginPath(), this._renderTextCommon(t, "strokeText"), t.closePath(), t.restore()); }, _getHeightOfLine: function () { return this._getHeightOfSingleLine() * this.lineHeight; }, _getHeightOfSingleLine: function () { return this.fontSize * this._fontSizeMult; }, _renderTextBackground: function (t) { this._renderBackground(t), this._renderTextLinesBackground(t); }, _renderTextLinesBackground: function (t) { if (this.textBackgroundColor) {
            var e, i, r, n = 0;
            t.fillStyle = this.textBackgroundColor;
            for (var s = 0, o = this._textLines.length; s < o; s++)
                e = this._getHeightOfLine(t, s), i = this._getLineWidth(t, s), i > 0 && (r = this._getLineLeftOffset(i), t.fillRect(this._getLeftOffset() + r, this._getTopOffset() + n, i, e / this.lineHeight)), n += e;
            this._removeShadow(t);
        } }, _getLineLeftOffset: function (t) { return "center" === this.textAlign ? (this.width - t) / 2 : "right" === this.textAlign ? this.width - t : 0; }, _clearCache: function () { this.__lineWidths = [], this.__lineHeights = []; }, _shouldClearCache: function () { var t = !1; if (this._forceClearCache)
            return this._forceClearCache = !1, !0; for (var e in this._dimensionAffectingProps)
            this["__" + e] !== this[e] && (this["__" + e] = this[e], t = !0); return t; }, _getLineWidth: function (t, e) { if (this.__lineWidths[e])
            return this.__lineWidths[e] === -1 ? this.width : this.__lineWidths[e]; var i, r, n = this._textLines[e]; return i = "" === n ? 0 : this._measureLine(t, e), this.__lineWidths[e] = i, i && "justify" === this.textAlign && (r = n.split(/\s+/), r.length > 1 && (this.__lineWidths[e] = -1)), i; }, _getWidthOfCharSpacing: function () { return 0 !== this.charSpacing ? this.fontSize * this.charSpacing / 1e3 : 0; }, _measureLine: function (t, e) { var i, r, n = this._textLines[e], s = t.measureText(n).width, o = 0; return 0 !== this.charSpacing && (i = n.split("").length, o = (i - 1) * this._getWidthOfCharSpacing()), r = s + o, r > 0 ? r : 0; }, _renderTextDecoration: function (t) { function e(e) { var n, s, o, a, h, c, l, u = 0; for (n = 0, s = r._textLines.length; n < s; n++) {
            for (h = r._getLineWidth(t, n), c = r._getLineLeftOffset(h), l = r._getHeightOfLine(t, n), o = 0, a = e.length; o < a; o++)
                t.fillRect(r._getLeftOffset() + c, u + (r._fontSizeMult - 1 + e[o]) * r.fontSize - i, h, r.fontSize / 15);
            u += l;
        } } if (this.textDecoration) {
            var i = this.height / 2, r = this, n = [];
            this.textDecoration.indexOf("underline") > -1 && n.push(.85), this.textDecoration.indexOf("line-through") > -1 && n.push(.43), this.textDecoration.indexOf("overline") > -1 && n.push(-.12), n.length > 0 && e(n);
        } }, _getFontDeclaration: function () { return [e.isLikelyNode ? this.fontWeight : this.fontStyle, e.isLikelyNode ? this.fontStyle : this.fontWeight, this.fontSize + "px", e.isLikelyNode ? '"' + this.fontFamily + '"' : this.fontFamily].join(" "); }, render: function (t, e) { this.visible && (t.save(), this._setTextStyles(t), this._shouldClearCache() && this._initDimensions(t), this.drawSelectionBackground(t), e || this.transform(t), this.transformMatrix && t.transform.apply(t, this.transformMatrix), this.group && "path-group" === this.group.type && t.translate(this.left, this.top), this._render(t), t.restore()); }, _splitTextIntoLines: function () { return this.text.split(this._reNewline); }, toObject: function (t) { var e = i(this.callSuper("toObject", t), { text: this.text, fontSize: this.fontSize, fontWeight: this.fontWeight, fontFamily: this.fontFamily, fontStyle: this.fontStyle, lineHeight: this.lineHeight, textDecoration: this.textDecoration, textAlign: this.textAlign, textBackgroundColor: this.textBackgroundColor, charSpacing: this.charSpacing }); return this.includeDefaultValues || this._removeDefaultValues(e), e; }, toSVG: function (t) { this.ctx || (this.ctx = e.util.createCanvasElement().getContext("2d")); var i = this._createBaseSVGMarkup(), r = this._getSVGLeftTopOffsets(this.ctx), n = this._getSVGTextAndBg(r.textTop, r.textLeft); return this._wrapSVGTextAndBg(i, n), t ? t(i.join("")) : i.join(""); }, _getSVGLeftTopOffsets: function (t) { var e = this._getHeightOfLine(t, 0), i = -this.width / 2, r = 0; return { textLeft: i + (this.group && "path-group" === this.group.type ? this.left : 0), textTop: r + (this.group && "path-group" === this.group.type ? -this.top : 0), lineTop: e }; }, _wrapSVGTextAndBg: function (t, e) { var i = !0, r = this.getSvgFilter(), n = "" === r ? "" : ' style="' + r + '"'; t.push("\t<g ", this.getSvgId(), 'transform="', this.getSvgTransform(), this.getSvgTransformMatrix(), '"', n, ">\n", e.textBgRects.join(""), "\t\t<text ", this.fontFamily ? 'font-family="' + this.fontFamily.replace(/"/g, "'") + '" ' : "", this.fontSize ? 'font-size="' + this.fontSize + '" ' : "", this.fontStyle ? 'font-style="' + this.fontStyle + '" ' : "", this.fontWeight ? 'font-weight="' + this.fontWeight + '" ' : "", this.textDecoration ? 'text-decoration="' + this.textDecoration + '" ' : "", 'style="', this.getSvgStyles(i), '" >\n', e.textSpans.join(""), "\t\t</text>\n", "\t</g>\n"); }, _getSVGTextAndBg: function (t, e) { var i = [], r = [], n = 0; this._setSVGBg(r); for (var s = 0, o = this._textLines.length; s < o; s++)
            this.textBackgroundColor && this._setSVGTextLineBg(r, s, e, t, n), this._setSVGTextLineText(s, i, n, e, t, r), n += this._getHeightOfLine(this.ctx, s); return { textSpans: i, textBgRects: r }; }, _setSVGTextLineText: function (t, i, r, o, a) { var h = this.fontSize * (this._fontSizeMult - this._fontSizeFraction) - a + r - this.height / 2; return "justify" === this.textAlign ? void this._setSVGTextLineJustifed(t, i, h, o) : void i.push('\t\t\t<tspan x="', n(o + this._getLineLeftOffset(this._getLineWidth(this.ctx, t)), s), '" ', 'y="', n(h, s), '" ', this._getFillAttributes(this.fill), ">", e.util.string.escapeXml(this._textLines[t]), "</tspan>\n"); }, _setSVGTextLineJustifed: function (t, i, r, o) { var a = e.util.createCanvasElement().getContext("2d"); this._setTextStyles(a); var h, c, l = this._textLines[t], u = l.split(/\s+/), f = this._getWidthOfWords(a, u.join("")), d = this.width - f, g = u.length - 1, p = g > 0 ? d / g : 0, v = this._getFillAttributes(this.fill); for (o += this._getLineLeftOffset(this._getLineWidth(a, t)), t = 0, c = u.length; t < c; t++)
            h = u[t], i.push('\t\t\t<tspan x="', n(o, s), '" ', 'y="', n(r, s), '" ', v, ">", e.util.string.escapeXml(h), "</tspan>\n"), o += this._getWidthOfWords(a, h) + p; }, _setSVGTextLineBg: function (t, e, i, r, o) { t.push("\t\t<rect ", this._getFillAttributes(this.textBackgroundColor), ' x="', n(i + this._getLineLeftOffset(this._getLineWidth(this.ctx, e)), s), '" y="', n(o - this.height / 2, s), '" width="', n(this._getLineWidth(this.ctx, e), s), '" height="', n(this._getHeightOfLine(this.ctx, e) / this.lineHeight, s), '"></rect>\n'); }, _setSVGBg: function (t) { this.backgroundColor && t.push("\t\t<rect ", this._getFillAttributes(this.backgroundColor), ' x="', n(-this.width / 2, s), '" y="', n(-this.height / 2, s), '" width="', n(this.width, s), '" height="', n(this.height, s), '"></rect>\n'); }, _getFillAttributes: function (t) { var i = t && "string" == typeof t ? new e.Color(t) : ""; return i && i.getSource() && 1 !== i.getAlpha() ? 'opacity="' + i.getAlpha() + '" fill="' + i.setAlpha(1).toRgb() + '"' : 'fill="' + t + '"'; }, _set: function (t, e) { this.callSuper("_set", t, e), t in this._dimensionAffectingProps && (this._initDimensions(), this.setCoords()); }, complexity: function () { return 1; } }), e.Text.ATTRIBUTE_NAMES = e.SHARED_ATTRIBUTES.concat("x y dx dy font-family font-style font-weight font-size text-decoration text-anchor".split(" ")), e.Text.DEFAULT_SVG_FONT_SIZE = 16, e.Text.fromElement = function (t, i) { if (!t)
        return null; var r = e.parseAttributes(t, e.Text.ATTRIBUTE_NAMES); i = e.util.object.extend(i ? e.util.object.clone(i) : {}, r), i.top = i.top || 0, i.left = i.left || 0, "dx" in r && (i.left += r.dx), "dy" in r && (i.top += r.dy), "fontSize" in i || (i.fontSize = e.Text.DEFAULT_SVG_FONT_SIZE), i.originX || (i.originX = "left"); var n = ""; "textContent" in t ? n = t.textContent : "firstChild" in t && null !== t.firstChild && "data" in t.firstChild && null !== t.firstChild.data && (n = t.firstChild.data), n = n.replace(/^\s+|\s+$|\n+/g, "").replace(/\s+/g, " "); var s = new e.Text(n, i), o = s.getHeight() / s.height, a = (s.height + s.strokeWidth) * s.lineHeight - s.height, h = a * o, c = s.getHeight() + h, l = 0; return "left" === s.originX && (l = s.getWidth() / 2), "right" === s.originX && (l = -s.getWidth() / 2), s.set({ left: s.getLeft() + l, top: s.getTop() - c / 2 + s.fontSize * (.18 + s._fontSizeFraction) / s.lineHeight }), s; }, e.Text.fromObject = function (t, i) { var n = new e.Text(t.text, r(t)); return i && i(n), n; }, e.util.createAccessors(e.Text);
}("undefined" != typeof exports ? exports : this), function () { var t = fabric.util.object.clone; fabric.IText = fabric.util.createClass(fabric.Text, fabric.Observable, { type: "i-text", selectionStart: 0, selectionEnd: 0, selectionColor: "rgba(17,119,255,0.3)", isEditing: !1, editable: !0, editingBorderColor: "rgba(102,153,255,0.25)", cursorWidth: 2, cursorColor: "#333", cursorDelay: 1e3, cursorDuration: 600, styles: null, caching: !0, _reSpace: /\s|\n/, _currentCursorOpacity: 0, _selectionDirection: null, _abortCursorAnimation: !1, __widthOfSpace: [], initialize: function (t, e) { this.styles = e ? e.styles || {} : {}, this.callSuper("initialize", t, e), this.initBehavior(); }, _clearCache: function () { this.callSuper("_clearCache"), this.__widthOfSpace = []; }, isEmptyStyles: function () { if (!this.styles)
        return !0; var t = this.styles; for (var e in t)
        for (var i in t[e])
            for (var r in t[e][i])
                return !1; return !0; }, setSelectionStart: function (t) { t = Math.max(t, 0), this._updateAndFire("selectionStart", t); }, setSelectionEnd: function (t) { t = Math.min(t, this.text.length), this._updateAndFire("selectionEnd", t); }, _updateAndFire: function (t, e) { this[t] !== e && (this._fireSelectionChanged(), this[t] = e), this._updateTextarea(); }, _fireSelectionChanged: function () { this.fire("selection:changed"), this.canvas && this.canvas.fire("text:selection:changed", { target: this }); }, getSelectionStyles: function (t, e) { if (2 === arguments.length) {
        for (var i = [], r = t; r < e; r++)
            i.push(this.getSelectionStyles(r));
        return i;
    } var n = this.get2DCursorLocation(t), s = this._getStyleDeclaration(n.lineIndex, n.charIndex); return s || {}; }, setSelectionStyles: function (t) { if (this.selectionStart === this.selectionEnd)
        this._extendStyles(this.selectionStart, t);
    else
        for (var e = this.selectionStart; e < this.selectionEnd; e++)
            this._extendStyles(e, t); return this._forceClearCache = !0, this; }, _extendStyles: function (t, e) { var i = this.get2DCursorLocation(t); this._getLineStyle(i.lineIndex) || this._setLineStyle(i.lineIndex, {}), this._getStyleDeclaration(i.lineIndex, i.charIndex) || this._setStyleDeclaration(i.lineIndex, i.charIndex, {}), fabric.util.object.extend(this._getStyleDeclaration(i.lineIndex, i.charIndex), e); }, _render: function (t) { this.oldWidth = this.width, this.oldHeight = this.height, this.callSuper("_render", t), this.ctx = t, this.cursorOffsetCache = {}, this.renderCursorOrSelection(); }, renderCursorOrSelection: function () { if (this.active && this.isEditing) {
        var t, e, i = this.text.split("");
        this.canvas.contextTop ? (e = this.canvas.contextTop, e.save(), e.transform.apply(e, this.canvas.viewportTransform), this.transform(e), this.transformMatrix && e.transform.apply(e, this.transformMatrix), this._clearTextArea(e)) : (e = this.ctx, e.save()), this.selectionStart === this.selectionEnd ? (t = this._getCursorBoundaries(i, "cursor"), this.renderCursor(t, e)) : (t = this._getCursorBoundaries(i, "selection"), this.renderSelection(i, t, e)), e.restore();
    } }, _clearTextArea: function (t) { var e = this.oldWidth + 4, i = this.oldHeight + 4; t.clearRect(-e / 2, -i / 2, e, i); }, get2DCursorLocation: function (t) { "undefined" == typeof t && (t = this.selectionStart); for (var e = this._textLines.length, i = 0; i < e; i++) {
        if (t <= this._textLines[i].length)
            return { lineIndex: i, charIndex: t };
        t -= this._textLines[i].length + 1;
    } return { lineIndex: i - 1, charIndex: this._textLines[i - 1].length < t ? this._textLines[i - 1].length : t }; }, getCurrentCharStyle: function (t, e) { var i = this._getStyleDeclaration(t, 0 === e ? 0 : e - 1); return { fontSize: i && i.fontSize || this.fontSize, fill: i && i.fill || this.fill, textBackgroundColor: i && i.textBackgroundColor || this.textBackgroundColor, textDecoration: i && i.textDecoration || this.textDecoration, fontFamily: i && i.fontFamily || this.fontFamily, fontWeight: i && i.fontWeight || this.fontWeight, fontStyle: i && i.fontStyle || this.fontStyle, stroke: i && i.stroke || this.stroke, strokeWidth: i && i.strokeWidth || this.strokeWidth }; }, getCurrentCharFontSize: function (t, e) { var i = this._getStyleDeclaration(t, 0 === e ? 0 : e - 1); return i && i.fontSize ? i.fontSize : this.fontSize; }, getCurrentCharColor: function (t, e) { var i = this._getStyleDeclaration(t, 0 === e ? 0 : e - 1); return i && i.fill ? i.fill : this.cursorColor; }, _getCursorBoundaries: function (t, e) { var i = Math.round(this._getLeftOffset()), r = this._getTopOffset(), n = this._getCursorBoundariesOffsets(t, e); return { left: i, top: r, leftOffset: n.left + n.lineLeft, topOffset: n.top }; }, _getCursorBoundariesOffsets: function (t, e) { if (this.cursorOffsetCache && "top" in this.cursorOffsetCache)
        return this.cursorOffsetCache; for (var i, r = 0, n = 0, s = 0, o = 0, a = 0, h = 0; h < this.selectionStart; h++)
        "\n" === t[h] ? (a = 0, o += this._getHeightOfLine(this.ctx, n), n++, s = 0) : (a += this._getWidthOfChar(this.ctx, t[h], n, s), s++), r = this._getLineLeftOffset(this._getLineWidth(this.ctx, n)); return "cursor" === e && (o += (1 - this._fontSizeFraction) * this._getHeightOfLine(this.ctx, n) / this.lineHeight - this.getCurrentCharFontSize(n, s) * (1 - this._fontSizeFraction)), 0 !== this.charSpacing && s === this._textLines[n].length && (a -= this._getWidthOfCharSpacing()), i = { top: o, left: a > 0 ? a : 0, lineLeft: r }, this.cursorOffsetCache = i, this.cursorOffsetCache; }, renderCursor: function (t, e) { var i = this.get2DCursorLocation(), r = i.lineIndex, n = i.charIndex, s = this.getCurrentCharFontSize(r, n), o = 0 === r && 0 === n ? this._getLineLeftOffset(this._getLineWidth(e, r)) : t.leftOffset, a = this.scaleX * this.canvas.getZoom(), h = this.cursorWidth / a; e.fillStyle = this.getCurrentCharColor(r, n), e.globalAlpha = this.__isMousedown ? 1 : this._currentCursorOpacity, e.fillRect(t.left + o - h / 2, t.top + t.topOffset, h, s); }, renderSelection: function (t, e, i) { i.fillStyle = this.selectionColor; for (var r = this.get2DCursorLocation(this.selectionStart), n = this.get2DCursorLocation(this.selectionEnd), s = r.lineIndex, o = n.lineIndex, a = s; a <= o; a++) {
        var h = this._getLineLeftOffset(this._getLineWidth(i, a)) || 0, c = this._getHeightOfLine(this.ctx, a), l = 0, u = 0, f = this._textLines[a];
        if (a === s) {
            for (var d = 0, g = f.length; d < g; d++)
                d >= r.charIndex && (a !== o || d < n.charIndex) && (u += this._getWidthOfChar(i, f[d], a, d)), d < r.charIndex && (h += this._getWidthOfChar(i, f[d], a, d));
            d === f.length && (u -= this._getWidthOfCharSpacing());
        }
        else if (a > s && a < o)
            u += this._getLineWidth(i, a) || 5;
        else if (a === o) {
            for (var p = 0, v = n.charIndex; p < v; p++)
                u += this._getWidthOfChar(i, f[p], a, p);
            n.charIndex === f.length && (u -= this._getWidthOfCharSpacing());
        }
        l = c, (this.lineHeight < 1 || a === o && this.lineHeight > 1) && (c /= this.lineHeight), i.fillRect(e.left + h, e.top + e.topOffset, u > 0 ? u : 0, c), e.topOffset += l;
    } }, _renderChars: function (t, e, i, r, n, s, o) { if (this.isEmptyStyles())
        return this._renderCharsFast(t, e, i, r, n); o = o || 0; var a, h, c = this._getHeightOfLine(e, s), l = ""; e.save(), n -= c / this.lineHeight * this._fontSizeFraction; for (var u = o, f = i.length + o; u <= f; u++)
        a = a || this.getCurrentCharStyle(s, u), h = this.getCurrentCharStyle(s, u + 1), (this._hasStyleChanged(a, h) || u === f) && (this._renderChar(t, e, s, u - 1, l, r, n, c), l = "", a = h), l += i[u - o]; e.restore(); }, _renderCharsFast: function (t, e, i, r, n) { "fillText" === t && this.fill && this.callSuper("_renderChars", t, e, i, r, n), "strokeText" === t && (this.stroke && this.strokeWidth > 0 || this.skipFillStrokeCheck) && this.callSuper("_renderChars", t, e, i, r, n); }, _renderChar: function (t, e, i, r, n, s, o, a) { var h, c, l, u, f, d, g, p, v, b = this._getStyleDeclaration(i, r); if (b ? (c = this._getHeightOfChar(e, n, i, r), u = b.stroke, l = b.fill, d = b.textDecoration) : c = this.fontSize, u = (u || this.stroke) && "strokeText" === t, l = (l || this.fill) && "fillText" === t, b && e.save(), h = this._applyCharStylesGetWidth(e, n, i, r, b || null), d = d || this.textDecoration, b && b.textBackgroundColor && this._removeShadow(e), 0 !== this.charSpacing) {
        p = this._getWidthOfCharSpacing(), g = n.split(""), h = 0;
        for (var m, y = 0, _ = g.length; y < _; y++)
            m = g[y], l && e.fillText(m, s + h, o), u && e.strokeText(m, s + h, o), v = e.measureText(m).width + p, h += v > 0 ? v : 0;
    }
    else
        l && e.fillText(n, s, o), u && e.strokeText(n, s, o); (d || "" !== d) && (f = this._fontSizeFraction * a / this.lineHeight, this._renderCharDecoration(e, d, s, o, f, h, c)), b && e.restore(), e.translate(h, 0); }, _hasStyleChanged: function (t, e) { return t.fill !== e.fill || t.fontSize !== e.fontSize || t.textBackgroundColor !== e.textBackgroundColor || t.textDecoration !== e.textDecoration || t.fontFamily !== e.fontFamily || t.fontWeight !== e.fontWeight || t.fontStyle !== e.fontStyle || t.stroke !== e.stroke || t.strokeWidth !== e.strokeWidth; }, _renderCharDecoration: function (t, e, i, r, n, s, o) { if (e) {
        var a, h, c = o / 15, l = { underline: r + o / 10, "line-through": r - o * (this._fontSizeFraction + this._fontSizeMult - 1) + c, overline: r - (this._fontSizeMult - this._fontSizeFraction) * o }, u = ["underline", "line-through", "overline"];
        for (a = 0; a < u.length; a++)
            h = u[a], e.indexOf(h) > -1 && t.fillRect(i, l[h], s, c);
    } }, _renderTextLine: function (t, e, i, r, n, s) { this.isEmptyStyles() || (n += this.fontSize * (this._fontSizeFraction + .03)), this.callSuper("_renderTextLine", t, e, i, r, n, s); }, _renderTextDecoration: function (t) { if (this.isEmptyStyles())
        return this.callSuper("_renderTextDecoration", t); }, _renderTextLinesBackground: function (t) { this.callSuper("_renderTextLinesBackground", t); for (var e, i, r, n, s, o, a = 0, h = this._getLeftOffset(), c = this._getTopOffset(), l = 0, u = this._textLines.length; l < u; l++)
        if (e = this._getHeightOfLine(t, l), n = this._textLines[l], "" !== n && this.styles && this._getLineStyle(l)) {
            i = this._getLineWidth(t, l), r = this._getLineLeftOffset(i);
            for (var f = 0, d = n.length; f < d; f++)
                o = this._getStyleDeclaration(l, f), o && o.textBackgroundColor && (s = n[f], t.fillStyle = o.textBackgroundColor, t.fillRect(h + r + this._getWidthOfCharsAt(t, l, f), c + a, this._getWidthOfChar(t, s, l, f) + 1, e / this.lineHeight));
            a += e;
        }
        else
            a += e; }, _getCacheProp: function (t, e) { return t + e.fontSize + e.fontWeight + e.fontStyle; }, _getFontCache: function (t) { return fabric.charWidthsCache[t] || (fabric.charWidthsCache[t] = {}), fabric.charWidthsCache[t]; }, _applyCharStylesGetWidth: function (e, i, r, n, s) { var o, a, h, c = s || this._getStyleDeclaration(r, n), l = t(c); if (this._applyFontStyles(l), h = this._getFontCache(l.fontFamily), a = this._getCacheProp(i, l), !c && h[a] && this.caching)
        return h[a]; "string" == typeof l.shadow && (l.shadow = new fabric.Shadow(l.shadow)); var u = l.fill || this.fill; return e.fillStyle = u.toLive ? u.toLive(e, this) : u, l.stroke && (e.strokeStyle = l.stroke && l.stroke.toLive ? l.stroke.toLive(e, this) : l.stroke), e.lineWidth = l.strokeWidth || this.strokeWidth, e.font = this._getFontDeclaration.call(l), l.shadow && (l.scaleX = this.scaleX, l.scaleY = this.scaleY, l.canvas = this.canvas, l.getObjectScaling = this.getObjectScaling, this._setShadow.call(l, e)), this.caching && h[a] ? h[a] : (o = e.measureText(i).width, this.caching && (h[a] = o), o); }, _applyFontStyles: function (t) { t.fontFamily || (t.fontFamily = this.fontFamily), t.fontSize || (t.fontSize = this.fontSize), t.fontWeight || (t.fontWeight = this.fontWeight), t.fontStyle || (t.fontStyle = this.fontStyle); }, _getStyleDeclaration: function (e, i, r) { return r ? this.styles[e] && this.styles[e][i] ? t(this.styles[e][i]) : {} : this.styles[e] && this.styles[e][i] ? this.styles[e][i] : null; }, _setStyleDeclaration: function (t, e, i) { this.styles[t][e] = i; }, _deleteStyleDeclaration: function (t, e) { delete this.styles[t][e]; }, _getLineStyle: function (t) { return this.styles[t]; }, _setLineStyle: function (t, e) { this.styles[t] = e; }, _deleteLineStyle: function (t) { delete this.styles[t]; }, _getWidthOfChar: function (t, e, i, r) { if (!this._isMeasuring && "justify" === this.textAlign && this._reSpacesAndTabs.test(e))
        return this._getWidthOfSpace(t, i); t.save(); var n = this._applyCharStylesGetWidth(t, e, i, r); return 0 !== this.charSpacing && (n += this._getWidthOfCharSpacing()), t.restore(), n > 0 ? n : 0; }, _getHeightOfChar: function (t, e, i) { var r = this._getStyleDeclaration(e, i); return r && r.fontSize ? r.fontSize : this.fontSize; }, _getWidthOfCharsAt: function (t, e, i) { var r, n, s = 0; for (r = 0; r < i; r++)
        n = this._textLines[e][r], s += this._getWidthOfChar(t, n, e, r); return s; }, _measureLine: function (t, e) { this._isMeasuring = !0; var i = this._getWidthOfCharsAt(t, e, this._textLines[e].length); return 0 !== this.charSpacing && (i -= this._getWidthOfCharSpacing()), this._isMeasuring = !1, i > 0 ? i : 0; }, _getWidthOfSpace: function (t, e) { if (this.__widthOfSpace[e])
        return this.__widthOfSpace[e]; var i = this._textLines[e], r = this._getWidthOfWords(t, i, e, 0), n = this.width - r, s = i.length - i.replace(this._reSpacesAndTabs, "").length, o = Math.max(n / s, t.measureText(" ").width); return this.__widthOfSpace[e] = o, o; }, _getWidthOfWords: function (t, e, i, r) { for (var n = 0, s = 0; s < e.length; s++) {
        var o = e[s];
        o.match(/\s/) || (n += this._getWidthOfChar(t, o, i, s + r));
    } return n; }, _getHeightOfLine: function (t, e) { if (this.__lineHeights[e])
        return this.__lineHeights[e]; for (var i = this._textLines[e], r = this._getHeightOfChar(t, e, 0), n = 1, s = i.length; n < s; n++) {
        var o = this._getHeightOfChar(t, e, n);
        o > r && (r = o);
    } return this.__lineHeights[e] = r * this.lineHeight * this._fontSizeMult, this.__lineHeights[e]; }, _getTextHeight: function (t) { for (var e, i = 0, r = 0, n = this._textLines.length; r < n; r++)
        e = this._getHeightOfLine(t, r), i += r === n - 1 ? e / this.lineHeight : e; return i; }, toObject: function (e) { var i, r, n, s = {}; for (i in this.styles) {
        n = this.styles[i], s[i] = {};
        for (r in n)
            s[i][r] = t(n[r]);
    } return fabric.util.object.extend(this.callSuper("toObject", e), { styles: s }); } }), fabric.IText.fromObject = function (e, i) { var r = new fabric.IText(e.text, t(e)); return i && i(r), r; }; }(), function () {
    var t = fabric.util.object.clone;
    fabric.util.object.extend(fabric.IText.prototype, { initBehavior: function () { this.initAddedHandler(), this.initRemovedHandler(), this.initCursorSelectionHandlers(), this.initDoubleClickSimulation(), this.mouseMoveHandler = this.mouseMoveHandler.bind(this); }, initSelectedHandler: function () { this.on("selected", function () { var t = this; setTimeout(function () { t.selected = !0; }, 100); }); }, initAddedHandler: function () { var t = this; this.on("added", function () { var e = t.canvas; e && (e._hasITextHandlers || (e._hasITextHandlers = !0, t._initCanvasHandlers(e)), e._iTextInstances = e._iTextInstances || [], e._iTextInstances.push(t)); }); }, initRemovedHandler: function () { var t = this; this.on("removed", function () { var e = t.canvas; e && (e._iTextInstances = e._iTextInstances || [], fabric.util.removeFromArray(e._iTextInstances, t), 0 === e._iTextInstances.length && (e._hasITextHandlers = !1, t._removeCanvasHandlers(e))); }); }, _initCanvasHandlers: function (t) { t._canvasITextSelectionClearedHanlder = function () { fabric.IText.prototype.exitEditingOnOthers(t); }.bind(this), t._mouseUpITextHandler = function () { t._iTextInstances && t._iTextInstances.forEach(function (t) { t.__isMousedown = !1; }); }.bind(this), t.on("selection:cleared", t._canvasITextSelectionClearedHanlder), t.on("object:selected", t._canvasITextSelectionClearedHanlder), t.on("mouse:up", t._mouseUpITextHandler); }, _removeCanvasHandlers: function (t) { t.off("selection:cleared", t._canvasITextSelectionClearedHanlder), t.off("object:selected", t._canvasITextSelectionClearedHanlder), t.off("mouse:up", t._mouseUpITextHandler); }, _tick: function () { this._currentTickState = this._animateCursor(this, 1, this.cursorDuration, "_onTickComplete"); }, _animateCursor: function (t, e, i, r) { var n; return n = { isAborted: !1, abort: function () { this.isAborted = !0; } }, t.animate("_currentCursorOpacity", e, { duration: i, onComplete: function () { n.isAborted || t[r](); }, onChange: function () { t.canvas && t.selectionStart === t.selectionEnd && t.renderCursorOrSelection(); }, abort: function () { return n.isAborted; } }), n; }, _onTickComplete: function () { var t = this; this._cursorTimeout1 && clearTimeout(this._cursorTimeout1), this._cursorTimeout1 = setTimeout(function () { t._currentTickCompleteState = t._animateCursor(t, 0, this.cursorDuration / 2, "_tick"); }, 100); }, initDelayedCursor: function (t) { var e = this, i = t ? 0 : this.cursorDelay; this.abortCursorAnimation(), this._currentCursorOpacity = 1, this._cursorTimeout2 = setTimeout(function () { e._tick(); }, i); }, abortCursorAnimation: function () { var t = this._currentTickState || this._currentTickCompleteState; this._currentTickState && this._currentTickState.abort(), this._currentTickCompleteState && this._currentTickCompleteState.abort(), clearTimeout(this._cursorTimeout1), clearTimeout(this._cursorTimeout2), this._currentCursorOpacity = 0, t && this.canvas && this.canvas.clearContext(this.canvas.contextTop || this.ctx); }, selectAll: function () { this.selectionStart = 0, this.selectionEnd = this.text.length, this._fireSelectionChanged(), this._updateTextarea(); }, getSelectedText: function () { return this.text.slice(this.selectionStart, this.selectionEnd); }, findWordBoundaryLeft: function (t) { var e = 0, i = t - 1; if (this._reSpace.test(this.text.charAt(i)))
            for (; this._reSpace.test(this.text.charAt(i));)
                e++, i--; for (; /\S/.test(this.text.charAt(i)) && i > -1;)
            e++, i--; return t - e; }, findWordBoundaryRight: function (t) { var e = 0, i = t; if (this._reSpace.test(this.text.charAt(i)))
            for (; this._reSpace.test(this.text.charAt(i));)
                e++, i++; for (; /\S/.test(this.text.charAt(i)) && i < this.text.length;)
            e++, i++; return t + e; }, findLineBoundaryLeft: function (t) { for (var e = 0, i = t - 1; !/\n/.test(this.text.charAt(i)) && i > -1;)
            e++, i--; return t - e; }, findLineBoundaryRight: function (t) { for (var e = 0, i = t; !/\n/.test(this.text.charAt(i)) && i < this.text.length;)
            e++, i++; return t + e; }, getNumNewLinesInSelectedText: function () { for (var t = this.getSelectedText(), e = 0, i = 0, r = t.length; i < r; i++)
            "\n" === t[i] && e++; return e; }, searchWordBoundary: function (t, e) { for (var i = this._reSpace.test(this.text.charAt(t)) ? t - 1 : t, r = this.text.charAt(i), n = /[ \n\.,;!\?\-]/; !n.test(r) && i > 0 && i < this.text.length;)
            i += e, r = this.text.charAt(i); return n.test(r) && "\n" !== r && (i += 1 === e ? 0 : 1), i; }, selectWord: function (t) { t = t || this.selectionStart; var e = this.searchWordBoundary(t, -1), i = this.searchWordBoundary(t, 1); this.selectionStart = e, this.selectionEnd = i, this._fireSelectionChanged(), this._updateTextarea(), this.renderCursorOrSelection(); }, selectLine: function (t) { t = t || this.selectionStart; var e = this.findLineBoundaryLeft(t), i = this.findLineBoundaryRight(t); this.selectionStart = e, this.selectionEnd = i, this._fireSelectionChanged(), this._updateTextarea(); }, enterEditing: function (t) { if (!this.isEditing && this.editable)
            return this.canvas && this.exitEditingOnOthers(this.canvas), this.isEditing = !0, this.initHiddenTextarea(t), this.hiddenTextarea.focus(), this._updateTextarea(), this._saveEditingProps(), this._setEditingProps(), this._textBeforeEdit = this.text, this._tick(), this.fire("editing:entered"), this.canvas ? (this.canvas.fire("text:editing:entered", { target: this }), this.initMouseMoveHandler(), this.canvas.renderAll(), this) : this; }, exitEditingOnOthers: function (t) { t._iTextInstances && t._iTextInstances.forEach(function (t) { t.selected = !1, t.isEditing && t.exitEditing(); }); }, initMouseMoveHandler: function () { this.canvas.on("mouse:move", this.mouseMoveHandler); }, mouseMoveHandler: function (t) { if (this.__isMousedown && this.isEditing) {
            var e = this.getSelectionStartFromPointer(t.e), i = this.selectionStart, r = this.selectionEnd;
            e !== this.__selectionStartOnMouseDown && (e > this.__selectionStartOnMouseDown ? (this.selectionStart = this.__selectionStartOnMouseDown, this.selectionEnd = e) : (this.selectionStart = e, this.selectionEnd = this.__selectionStartOnMouseDown), this.selectionStart === i && this.selectionEnd === r || (this._fireSelectionChanged(), this._updateTextarea(), this.renderCursorOrSelection()));
        } }, _setEditingProps: function () { this.hoverCursor = "text", this.canvas && (this.canvas.defaultCursor = this.canvas.moveCursor = "text"), this.borderColor = this.editingBorderColor, this.hasControls = this.selectable = !1, this.lockMovementX = this.lockMovementY = !0; }, _updateTextarea: function () { if (this.hiddenTextarea && !this.inCompositionMode && (this.cursorOffsetCache = {}, this.hiddenTextarea.value = this.text, this.hiddenTextarea.selectionStart = this.selectionStart, this.hiddenTextarea.selectionEnd = this.selectionEnd, this.selectionStart === this.selectionEnd)) {
            var t = this._calcTextareaPosition();
            this.hiddenTextarea.style.left = t.left, this.hiddenTextarea.style.top = t.top, this.hiddenTextarea.style.fontSize = t.fontSize;
        } }, _calcTextareaPosition: function () { if (!this.canvas)
            return { x: 1, y: 1 }; var t = this.text.split(""), e = this._getCursorBoundaries(t, "cursor"), i = this.get2DCursorLocation(), r = i.lineIndex, n = i.charIndex, s = this.getCurrentCharFontSize(r, n), o = 0 === r && 0 === n ? this._getLineLeftOffset(this._getLineWidth(this.ctx, r)) : e.leftOffset, a = this.calcTransformMatrix(), h = { x: e.left + o, y: e.top + e.topOffset + s }, c = this.canvas.upperCanvasEl, l = c.width - s, u = c.height - s; return h = fabric.util.transformPoint(h, a), h = fabric.util.transformPoint(h, this.canvas.viewportTransform), h.x < 0 && (h.x = 0), h.x > l && (h.x = l), h.y < 0 && (h.y = 0), h.y > u && (h.y = u), h.x += this.canvas._offset.left, h.y += this.canvas._offset.top, { left: h.x + "px", top: h.y + "px", fontSize: s }; }, _saveEditingProps: function () {
            this._savedProps = { hasControls: this.hasControls, borderColor: this.borderColor, lockMovementX: this.lockMovementX, lockMovementY: this.lockMovementY, hoverCursor: this.hoverCursor, defaultCursor: this.canvas && this.canvas.defaultCursor, moveCursor: this.canvas && this.canvas.moveCursor
            };
        }, _restoreEditingProps: function () { this._savedProps && (this.hoverCursor = this._savedProps.overCursor, this.hasControls = this._savedProps.hasControls, this.borderColor = this._savedProps.borderColor, this.lockMovementX = this._savedProps.lockMovementX, this.lockMovementY = this._savedProps.lockMovementY, this.canvas && (this.canvas.defaultCursor = this._savedProps.defaultCursor, this.canvas.moveCursor = this._savedProps.moveCursor)); }, exitEditing: function () { var t = this._textBeforeEdit !== this.text; return this.selected = !1, this.isEditing = !1, this.selectable = !0, this.selectionEnd = this.selectionStart, this.hiddenTextarea && this.canvas && this.hiddenTextarea.parentNode.removeChild(this.hiddenTextarea), this.hiddenTextarea = null, this.abortCursorAnimation(), this._restoreEditingProps(), this._currentCursorOpacity = 0, this.fire("editing:exited"), t && this.fire("modified"), this.canvas && (this.canvas.off("mouse:move", this.mouseMoveHandler), this.canvas.fire("text:editing:exited", { target: this }), t && this.canvas.fire("object:modified", { target: this })), this; }, _removeExtraneousStyles: function () { for (var t in this.styles)
            this._textLines[t] || delete this.styles[t]; }, _removeCharsFromTo: function (t, e) { for (; e !== t;)
            this._removeSingleCharAndStyle(t + 1), e--; this.selectionStart = t, this.selectionEnd = t; }, _removeSingleCharAndStyle: function (t) { var e = "\n" === this.text[t - 1], i = e ? t : t - 1; this.removeStyleObject(e, i), this.text = this.text.slice(0, t - 1) + this.text.slice(t), this._textLines = this._splitTextIntoLines(); }, insertChars: function (t, e) { var i; if (this.selectionEnd - this.selectionStart > 1 && this._removeCharsFromTo(this.selectionStart, this.selectionEnd), !e && this.isEmptyStyles())
            return void this.insertChar(t, !1); for (var r = 0, n = t.length; r < n; r++)
            e && (i = fabric.copiedTextStyle[r]), this.insertChar(t[r], r < n - 1, i); }, insertChar: function (t, e, i) { var r = "\n" === this.text[this.selectionStart]; this.text = this.text.slice(0, this.selectionStart) + t + this.text.slice(this.selectionEnd), this._textLines = this._splitTextIntoLines(), this.insertStyleObjects(t, r, i), this.selectionStart += t.length, this.selectionEnd = this.selectionStart, e || (this._updateTextarea(), this.setCoords(), this._fireSelectionChanged(), this.fire("changed"), this.canvas && this.canvas.fire("text:changed", { target: this }), this.canvas && this.canvas.renderAll()); }, insertNewlineStyleObject: function (e, i, r) { this.shiftLineStyles(e, 1), this.styles[e + 1] || (this.styles[e + 1] = {}); var n = {}, s = {}; if (this.styles[e] && this.styles[e][i - 1] && (n = this.styles[e][i - 1]), r)
            s[0] = t(n), this.styles[e + 1] = s;
        else {
            for (var o in this.styles[e])
                parseInt(o, 10) >= i && (s[parseInt(o, 10) - i] = this.styles[e][o], delete this.styles[e][o]);
            this.styles[e + 1] = s;
        } this._forceClearCache = !0; }, insertCharStyleObject: function (e, i, r) { var n = this.styles[e], s = t(n); 0 !== i || r || (i = 1); for (var o in s) {
            var a = parseInt(o, 10);
            a >= i && (n[a + 1] = s[a], s[a - 1] || delete n[a]);
        } this.styles[e][i] = r || t(n[i - 1]), this._forceClearCache = !0; }, insertStyleObjects: function (t, e, i) { var r = this.get2DCursorLocation(), n = r.lineIndex, s = r.charIndex; this._getLineStyle(n) || this._setLineStyle(n, {}), "\n" === t ? this.insertNewlineStyleObject(n, s, e) : this.insertCharStyleObject(n, s, i); }, shiftLineStyles: function (e, i) { var r = t(this.styles); for (var n in this.styles) {
            var s = parseInt(n, 10);
            s > e && (this.styles[s + i] = r[s], r[s - i] || delete this.styles[s]);
        } }, removeStyleObject: function (t, e) { var i = this.get2DCursorLocation(e), r = i.lineIndex, n = i.charIndex; this._removeStyleObject(t, i, r, n); }, _getTextOnPreviousLine: function (t) { return this._textLines[t - 1]; }, _removeStyleObject: function (e, i, r, n) { if (e) {
            var s = this._getTextOnPreviousLine(i.lineIndex), o = s ? s.length : 0;
            this.styles[r - 1] || (this.styles[r - 1] = {});
            for (n in this.styles[r])
                this.styles[r - 1][parseInt(n, 10) + o] = this.styles[r][n];
            this.shiftLineStyles(i.lineIndex, -1);
        }
        else {
            var a = this.styles[r];
            a && delete a[n];
            var h = t(a);
            for (var c in h) {
                var l = parseInt(c, 10);
                l >= n && 0 !== l && (a[l - 1] = h[l], delete a[l]);
            }
        } }, insertNewline: function () { this.insertChars("\n"); }, setSelectionStartEndWithShift: function (t, e, i) { i <= t ? (e === t ? this._selectionDirection = "left" : "right" === this._selectionDirection && (this._selectionDirection = "left", this.selectionEnd = t), this.selectionStart = i) : i > t && i < e ? "right" === this._selectionDirection ? this.selectionEnd = i : this.selectionStart = i : (e === t ? this._selectionDirection = "right" : "left" === this._selectionDirection && (this._selectionDirection = "right", this.selectionStart = e), this.selectionEnd = i); }, setSelectionInBoundaries: function () { var t = this.text.length; this.selectionStart > t ? this.selectionStart = t : this.selectionStart < 0 && (this.selectionStart = 0), this.selectionEnd > t ? this.selectionEnd = t : this.selectionEnd < 0 && (this.selectionEnd = 0); } });
}(), fabric.util.object.extend(fabric.IText.prototype, { initDoubleClickSimulation: function () { this.__lastClickTime = +new Date, this.__lastLastClickTime = +new Date, this.__lastPointer = {}, this.on("mousedown", this.onMouseDown.bind(this)); }, onMouseDown: function (t) { this.__newClickTime = +new Date; var e = this.canvas.getPointer(t.e); this.isTripleClick(e) ? (this.fire("tripleclick", t), this._stopEvent(t.e)) : this.isDoubleClick(e) && (this.fire("dblclick", t), this._stopEvent(t.e)), this.__lastLastClickTime = this.__lastClickTime, this.__lastClickTime = this.__newClickTime, this.__lastPointer = e, this.__lastIsEditing = this.isEditing, this.__lastSelected = this.selected; }, isDoubleClick: function (t) { return this.__newClickTime - this.__lastClickTime < 500 && this.__lastPointer.x === t.x && this.__lastPointer.y === t.y && this.__lastIsEditing; }, isTripleClick: function (t) { return this.__newClickTime - this.__lastClickTime < 500 && this.__lastClickTime - this.__lastLastClickTime < 500 && this.__lastPointer.x === t.x && this.__lastPointer.y === t.y; }, _stopEvent: function (t) { t.preventDefault && t.preventDefault(), t.stopPropagation && t.stopPropagation(); }, initCursorSelectionHandlers: function () { this.initSelectedHandler(), this.initMousedownHandler(), this.initMouseupHandler(), this.initClicks(); }, initClicks: function () { this.on("dblclick", function (t) { this.selectWord(this.getSelectionStartFromPointer(t.e)); }), this.on("tripleclick", function (t) { this.selectLine(this.getSelectionStartFromPointer(t.e)); }); }, initMousedownHandler: function () { this.on("mousedown", function (t) { if (this.editable) {
        var e = this.canvas.getPointer(t.e);
        this.__mousedownX = e.x, this.__mousedownY = e.y, this.__isMousedown = !0, this.selected && this.setCursorByClick(t.e), this.isEditing && (this.__selectionStartOnMouseDown = this.selectionStart, this.selectionStart === this.selectionEnd && this.abortCursorAnimation(), this.renderCursorOrSelection());
    } }); }, _isObjectMoved: function (t) { var e = this.canvas.getPointer(t); return this.__mousedownX !== e.x || this.__mousedownY !== e.y; }, initMouseupHandler: function () { this.on("mouseup", function (t) { this.__isMousedown = !1, this.editable && !this._isObjectMoved(t.e) && (this.__lastSelected && !this.__corner && (this.enterEditing(t.e), this.selectionStart === this.selectionEnd ? this.initDelayedCursor(!0) : this.renderCursorOrSelection()), this.selected = !0); }); }, setCursorByClick: function (t) { var e = this.getSelectionStartFromPointer(t), i = this.selectionStart, r = this.selectionEnd; t.shiftKey ? this.setSelectionStartEndWithShift(i, r, e) : (this.selectionStart = e, this.selectionEnd = e), this._fireSelectionChanged(), this._updateTextarea(); }, getSelectionStartFromPointer: function (t) { for (var e, i, r = this.getLocalPointer(t), n = 0, s = 0, o = 0, a = 0, h = 0, c = this._textLines.length; h < c; h++) {
        i = this._textLines[h], o += this._getHeightOfLine(this.ctx, h) * this.scaleY;
        var l = this._getLineWidth(this.ctx, h), u = this._getLineLeftOffset(l);
        s = u * this.scaleX;
        for (var f = 0, d = i.length; f < d; f++) {
            if (n = s, s += this._getWidthOfChar(this.ctx, i[f], h, this.flipX ? d - f : f) * this.scaleX, !(o <= r.y || s <= r.x))
                return this._getNewSelectionStartFromOffset(r, n, s, a + h, d);
            a++;
        }
        if (r.y < o)
            return this._getNewSelectionStartFromOffset(r, n, s, a + h - 1, d);
    } if ("undefined" == typeof e)
        return this.text.length; }, _getNewSelectionStartFromOffset: function (t, e, i, r, n) { var s = t.x - e, o = i - t.x, a = o > s ? 0 : 1, h = r + a; return this.flipX && (h = n - h), h > this.text.length && (h = this.text.length), h; } }), fabric.util.object.extend(fabric.IText.prototype, { initHiddenTextarea: function () { this.hiddenTextarea = fabric.document.createElement("textarea"), this.hiddenTextarea.setAttribute("autocapitalize", "off"); var t = this._calcTextareaPosition(); this.hiddenTextarea.style.cssText = "position: absolute; top: " + t.top + "; left: " + t.left + "; opacity: 0; width: 0px; height: 0px; z-index: -999;", fabric.document.body.appendChild(this.hiddenTextarea), fabric.util.addListener(this.hiddenTextarea, "keydown", this.onKeyDown.bind(this)), fabric.util.addListener(this.hiddenTextarea, "keyup", this.onKeyUp.bind(this)), fabric.util.addListener(this.hiddenTextarea, "input", this.onInput.bind(this)), fabric.util.addListener(this.hiddenTextarea, "copy", this.copy.bind(this)), fabric.util.addListener(this.hiddenTextarea, "cut", this.cut.bind(this)), fabric.util.addListener(this.hiddenTextarea, "paste", this.paste.bind(this)), fabric.util.addListener(this.hiddenTextarea, "compositionstart", this.onCompositionStart.bind(this)), fabric.util.addListener(this.hiddenTextarea, "compositionupdate", this.onCompositionUpdate.bind(this)), fabric.util.addListener(this.hiddenTextarea, "compositionend", this.onCompositionEnd.bind(this)), !this._clickHandlerInitialized && this.canvas && (fabric.util.addListener(this.canvas.upperCanvasEl, "click", this.onClick.bind(this)), this._clickHandlerInitialized = !0); }, _keysMap: { 8: "removeChars", 9: "exitEditing", 27: "exitEditing", 13: "insertNewline", 33: "moveCursorUp", 34: "moveCursorDown", 35: "moveCursorRight", 36: "moveCursorLeft", 37: "moveCursorLeft", 38: "moveCursorUp", 39: "moveCursorRight", 40: "moveCursorDown", 46: "forwardDelete" }, _ctrlKeysMapUp: { 67: "copy", 88: "cut" }, _ctrlKeysMapDown: { 65: "selectAll" }, onClick: function () { this.hiddenTextarea && this.hiddenTextarea.focus(); }, onKeyDown: function (t) { if (this.isEditing) {
        if (t.keyCode in this._keysMap)
            this[this._keysMap[t.keyCode]](t);
        else {
            if (!(t.keyCode in this._ctrlKeysMapDown && (t.ctrlKey || t.metaKey)))
                return;
            this[this._ctrlKeysMapDown[t.keyCode]](t);
        }
        t.stopImmediatePropagation(), t.preventDefault(), this.canvas && this.canvas.renderAll();
    } }, onKeyUp: function (t) { return !this.isEditing || this._copyDone ? void (this._copyDone = !1) : void (t.keyCode in this._ctrlKeysMapUp && (t.ctrlKey || t.metaKey) && (this[this._ctrlKeysMapUp[t.keyCode]](t), t.stopImmediatePropagation(), t.preventDefault(), this.canvas && this.canvas.renderAll())); }, onInput: function (t) { if (this.isEditing && !this.inCompositionMode) {
        var e, i, r, n = this.selectionStart || 0, s = this.selectionEnd || 0, o = this.text.length, a = this.hiddenTextarea.value.length;
        a > o ? (r = "left" === this._selectionDirection ? s : n, e = a - o, i = this.hiddenTextarea.value.slice(r, r + e)) : (e = a - o + s - n, i = this.hiddenTextarea.value.slice(n, n + e)), this.insertChars(i), t.stopPropagation();
    } }, onCompositionStart: function () { this.inCompositionMode = !0, this.prevCompositionLength = 0, this.compositionStart = this.selectionStart; }, onCompositionEnd: function () { this.inCompositionMode = !1; }, onCompositionUpdate: function (t) { var e = t.data; this.selectionStart = this.compositionStart, this.selectionEnd = this.selectionEnd === this.selectionStart ? this.compositionStart + this.prevCompositionLength : this.selectionEnd, this.insertChars(e, !1), this.prevCompositionLength = e.length; }, forwardDelete: function (t) { if (this.selectionStart === this.selectionEnd) {
        if (this.selectionStart === this.text.length)
            return;
        this.moveCursorRight(t);
    } this.removeChars(t); }, copy: function (t) { if (this.selectionStart !== this.selectionEnd) {
        var e = this.getSelectedText(), i = this._getClipboardData(t);
        i && i.setData("text", e), fabric.copiedText = e, fabric.copiedTextStyle = this.getSelectionStyles(this.selectionStart, this.selectionEnd), t.stopImmediatePropagation(), t.preventDefault(), this._copyDone = !0;
    } }, paste: function (t) { var e = null, i = this._getClipboardData(t), r = !0; i ? (e = i.getData("text").replace(/\r/g, ""), fabric.copiedTextStyle && fabric.copiedText === e || (r = !1)) : e = fabric.copiedText, e && this.insertChars(e, r), t.stopImmediatePropagation(), t.preventDefault(); }, cut: function (t) { this.selectionStart !== this.selectionEnd && (this.copy(t), this.removeChars(t)); }, _getClipboardData: function (t) { return t && t.clipboardData || fabric.window.clipboardData; }, _getWidthBeforeCursor: function (t, e) { for (var i, r = this._textLines[t].slice(0, e), n = this._getLineWidth(this.ctx, t), s = this._getLineLeftOffset(n), o = 0, a = r.length; o < a; o++)
        i = r[o], s += this._getWidthOfChar(this.ctx, i, t, o); return s; }, getDownCursorOffset: function (t, e) { var i = this._getSelectionForOffset(t, e), r = this.get2DCursorLocation(i), n = r.lineIndex; if (n === this._textLines.length - 1 || t.metaKey || 34 === t.keyCode)
        return this.text.length - i; var s = r.charIndex, o = this._getWidthBeforeCursor(n, s), a = this._getIndexOnLine(n + 1, o), h = this._textLines[n].slice(s); return h.length + a + 2; }, _getSelectionForOffset: function (t, e) { return t.shiftKey && this.selectionStart !== this.selectionEnd && e ? this.selectionEnd : this.selectionStart; }, getUpCursorOffset: function (t, e) { var i = this._getSelectionForOffset(t, e), r = this.get2DCursorLocation(i), n = r.lineIndex; if (0 === n || t.metaKey || 33 === t.keyCode)
        return -i; var s = r.charIndex, o = this._getWidthBeforeCursor(n, s), a = this._getIndexOnLine(n - 1, o), h = this._textLines[n].slice(0, s); return -this._textLines[n - 1].length + a - h.length; }, _getIndexOnLine: function (t, e) { for (var i, r = this._getLineWidth(this.ctx, t), n = this._textLines[t], s = this._getLineLeftOffset(r), o = s, a = 0, h = 0, c = n.length; h < c; h++) {
        var l = n[h], u = this._getWidthOfChar(this.ctx, l, t, h);
        if (o += u, o > e) {
            i = !0;
            var f = o - u, d = o, g = Math.abs(f - e), p = Math.abs(d - e);
            a = p < g ? h : h - 1;
            break;
        }
    } return i || (a = n.length - 1), a; }, moveCursorDown: function (t) { this.selectionStart >= this.text.length && this.selectionEnd >= this.text.length || this._moveCursorUpOrDown("Down", t); }, moveCursorUp: function (t) { 0 === this.selectionStart && 0 === this.selectionEnd || this._moveCursorUpOrDown("Up", t); }, _moveCursorUpOrDown: function (t, e) { var i = "get" + t + "CursorOffset", r = this[i](e, "right" === this._selectionDirection); e.shiftKey ? this.moveCursorWithShift(r) : this.moveCursorWithoutShift(r), 0 !== r && (this.setSelectionInBoundaries(), this.abortCursorAnimation(), this._currentCursorOpacity = 1, this.initDelayedCursor(), this._fireSelectionChanged(), this._updateTextarea()); }, moveCursorWithShift: function (t) { var e = "left" === this._selectionDirection ? this.selectionStart + t : this.selectionEnd + t; return this.setSelectionStartEndWithShift(this.selectionStart, this.selectionEnd, e), 0 !== t; }, moveCursorWithoutShift: function (t) { return t < 0 ? (this.selectionStart += t, this.selectionEnd = this.selectionStart) : (this.selectionEnd += t, this.selectionStart = this.selectionEnd), 0 !== t; }, moveCursorLeft: function (t) { 0 === this.selectionStart && 0 === this.selectionEnd || this._moveCursorLeftOrRight("Left", t); }, _move: function (t, e, i) { var r; if (t.altKey)
        r = this["findWordBoundary" + i](this[e]);
    else {
        if (!t.metaKey && 35 !== t.keyCode && 36 !== t.keyCode)
            return this[e] += "Left" === i ? -1 : 1, !0;
        r = this["findLineBoundary" + i](this[e]);
    } if (void 0 !== typeof r && this[e] !== r)
        return this[e] = r, !0; }, _moveLeft: function (t, e) { return this._move(t, e, "Left"); }, _moveRight: function (t, e) { return this._move(t, e, "Right"); }, moveCursorLeftWithoutShift: function (t) { var e = !0; return this._selectionDirection = "left", this.selectionEnd === this.selectionStart && 0 !== this.selectionStart && (e = this._moveLeft(t, "selectionStart")), this.selectionEnd = this.selectionStart, e; }, moveCursorLeftWithShift: function (t) { return "right" === this._selectionDirection && this.selectionStart !== this.selectionEnd ? this._moveLeft(t, "selectionEnd") : 0 !== this.selectionStart ? (this._selectionDirection = "left", this._moveLeft(t, "selectionStart")) : void 0; }, moveCursorRight: function (t) { this.selectionStart >= this.text.length && this.selectionEnd >= this.text.length || this._moveCursorLeftOrRight("Right", t); }, _moveCursorLeftOrRight: function (t, e) { var i = "moveCursor" + t + "With"; this._currentCursorOpacity = 1, i += e.shiftKey ? "Shift" : "outShift", this[i](e) && (this.abortCursorAnimation(), this.initDelayedCursor(), this._fireSelectionChanged(), this._updateTextarea()); }, moveCursorRightWithShift: function (t) { return "left" === this._selectionDirection && this.selectionStart !== this.selectionEnd ? this._moveRight(t, "selectionStart") : this.selectionEnd !== this.text.length ? (this._selectionDirection = "right", this._moveRight(t, "selectionEnd")) : void 0; }, moveCursorRightWithoutShift: function (t) { var e = !0; return this._selectionDirection = "right", this.selectionStart === this.selectionEnd ? (e = this._moveRight(t, "selectionStart"), this.selectionEnd = this.selectionStart) : this.selectionStart = this.selectionEnd, e; }, removeChars: function (t) { this.selectionStart === this.selectionEnd ? this._removeCharsNearCursor(t) : this._removeCharsFromTo(this.selectionStart, this.selectionEnd), this.setSelectionEnd(this.selectionStart), this._removeExtraneousStyles(), this.canvas && this.canvas.renderAll(), this.setCoords(), this.fire("changed"), this.canvas && this.canvas.fire("text:changed", { target: this }); }, _removeCharsNearCursor: function (t) { if (0 !== this.selectionStart)
        if (t.metaKey) {
            var e = this.findLineBoundaryLeft(this.selectionStart);
            this._removeCharsFromTo(e, this.selectionStart), this.setSelectionStart(e);
        }
        else if (t.altKey) {
            var i = this.findWordBoundaryLeft(this.selectionStart);
            this._removeCharsFromTo(i, this.selectionStart), this.setSelectionStart(i);
        }
        else
            this._removeSingleCharAndStyle(this.selectionStart), this.setSelectionStart(this.selectionStart - 1); } }), function () { var t = fabric.util.toFixed, e = fabric.Object.NUM_FRACTION_DIGITS; fabric.util.object.extend(fabric.IText.prototype, { _setSVGTextLineText: function (t, e, i, r, n, s) { this._getLineStyle(t) ? this._setSVGTextLineChars(t, e, i, r, s) : fabric.Text.prototype._setSVGTextLineText.call(this, t, e, i, r, n); }, _setSVGTextLineChars: function (t, e, i, r, n) { for (var s = this._textLines[t], o = 0, a = this._getLineLeftOffset(this._getLineWidth(this.ctx, t)) - this.width / 2, h = this._getSVGLineTopOffset(t), c = this._getHeightOfLine(this.ctx, t), l = 0, u = s.length; l < u; l++) {
        var f = this._getStyleDeclaration(t, l) || {};
        e.push(this._createTextCharSpan(s[l], f, a, h.lineTop + h.offset, o));
        var d = this._getWidthOfChar(this.ctx, s[l], t, l);
        f.textBackgroundColor && n.push(this._createTextCharBg(f, a, h.lineTop, c, d, o)), o += d;
    } }, _getSVGLineTopOffset: function (t) { for (var e = 0, i = 0, r = 0; r < t; r++)
        e += this._getHeightOfLine(this.ctx, r); return i = this._getHeightOfLine(this.ctx, r), { lineTop: e, offset: (this._fontSizeMult - this._fontSizeFraction) * i / (this.lineHeight * this._fontSizeMult) }; }, _createTextCharBg: function (i, r, n, s, o, a) { return ['\t\t<rect fill="', i.textBackgroundColor, '" x="', t(r + a, e), '" y="', t(n - this.height / 2, e), '" width="', t(o, e), '" height="', t(s / this.lineHeight, e), '"></rect>\n'].join(""); }, _createTextCharSpan: function (i, r, n, s, o) { var a = this.getSvgStyles.call(fabric.util.object.extend({ visible: !0, fill: this.fill, stroke: this.stroke, type: "text", getSvgFilter: fabric.Object.prototype.getSvgFilter }, r)); return ['\t\t\t<tspan x="', t(n + o, e), '" y="', t(s - this.height / 2, e), '" ', r.fontFamily ? 'font-family="' + r.fontFamily.replace(/"/g, "'") + '" ' : "", r.fontSize ? 'font-size="' + r.fontSize + '" ' : "", r.fontStyle ? 'font-style="' + r.fontStyle + '" ' : "", r.fontWeight ? 'font-weight="' + r.fontWeight + '" ' : "", r.textDecoration ? 'text-decoration="' + r.textDecoration + '" ' : "", 'style="', a, '">', fabric.util.string.escapeXml(i), "</tspan>\n"].join(""); } }); }(), function (t) {
    "use strict";
    var e = t.fabric || (t.fabric = {}), i = e.util.object.clone;
    e.Textbox = e.util.createClass(e.IText, e.Observable, { type: "textbox", minWidth: 20, dynamicMinWidth: 2, __cachedLines: null, lockScalingY: !0, lockScalingFlip: !0, initialize: function (t, i) { this.ctx = e.util.createCanvasElement().getContext("2d"), this.callSuper("initialize", t, i), this.setControlsVisibility(e.Textbox.getTextboxControlVisibility()), this._dimensionAffectingProps.width = !0; }, _initDimensions: function (t) { this.__skipDimension || (t || (t = e.util.createCanvasElement().getContext("2d"), this._setTextStyles(t)), this.dynamicMinWidth = 0, this._textLines = this._splitTextIntoLines(), this.dynamicMinWidth > this.width && this._set("width", this.dynamicMinWidth), this._clearCache(), this.height = this._getTextHeight(t)); }, _generateStyleMap: function () { for (var t = 0, e = 0, i = 0, r = {}, n = 0; n < this._textLines.length; n++)
            "\n" === this.text[i] && n > 0 ? (e = 0, i++, t++) : " " === this.text[i] && n > 0 && (e++, i++), r[n] = { line: t, offset: e }, i += this._textLines[n].length, e += this._textLines[n].length; return r; }, _getStyleDeclaration: function (t, e, i) { if (this._styleMap) {
            var r = this._styleMap[t];
            if (!r)
                return i ? {} : null;
            t = r.line, e = r.offset + e;
        } return this.callSuper("_getStyleDeclaration", t, e, i); }, _setStyleDeclaration: function (t, e, i) { var r = this._styleMap[t]; t = r.line, e = r.offset + e, this.styles[t][e] = i; }, _deleteStyleDeclaration: function (t, e) { var i = this._styleMap[t]; t = i.line, e = i.offset + e, delete this.styles[t][e]; }, _getLineStyle: function (t) { var e = this._styleMap[t]; return this.styles[e.line]; }, _setLineStyle: function (t, e) { var i = this._styleMap[t]; this.styles[i.line] = e; }, _deleteLineStyle: function (t) { var e = this._styleMap[t]; delete this.styles[e.line]; }, _wrapText: function (t, e) { var i, r = e.split(this._reNewline), n = []; for (i = 0; i < r.length; i++)
            n = n.concat(this._wrapLine(t, r[i], i)); return n; }, _measureText: function (t, e, i, r) { var n = 0; r = r || 0; for (var s = 0, o = e.length; s < o; s++)
            n += this._getWidthOfChar(t, e[s], i, s + r); return n; }, _wrapLine: function (t, e, i) { for (var r = 0, n = [], s = "", o = e.split(" "), a = "", h = 0, c = " ", l = 0, u = 0, f = 0, d = !0, g = this._getWidthOfCharSpacing(), p = 0; p < o.length; p++)
            a = o[p], l = this._measureText(t, a, i, h), h += a.length, r += u + l - g, r >= this.width && !d ? (n.push(s), s = "", r = l, d = !0) : r += g, d || (s += c), s += a, u = this._measureText(t, c, i, h), h++, d = !1, l > f && (f = l); return p && n.push(s), f > this.dynamicMinWidth && (this.dynamicMinWidth = f - g), n; }, _splitTextIntoLines: function () { var t = this.textAlign; this.ctx.save(), this._setTextStyles(this.ctx), this.textAlign = "left"; var e = this._wrapText(this.ctx, this.text); return this.textAlign = t, this.ctx.restore(), this._textLines = e, this._styleMap = this._generateStyleMap(), e; }, setOnGroup: function (t, e) { "scaleX" === t && (this.set("scaleX", Math.abs(1 / e)), this.set("width", this.get("width") * e / ("undefined" == typeof this.__oldScaleX ? 1 : this.__oldScaleX)), this.__oldScaleX = e); }, get2DCursorLocation: function (t) { "undefined" == typeof t && (t = this.selectionStart); for (var e = this._textLines.length, i = 0, r = 0; r < e; r++) {
            var n = this._textLines[r], s = n.length;
            if (t <= i + s)
                return { lineIndex: r, charIndex: t - i };
            i += s, "\n" !== this.text[i] && " " !== this.text[i] || i++;
        } return { lineIndex: e - 1, charIndex: this._textLines[e - 1].length }; }, _getCursorBoundariesOffsets: function (t, e) { for (var i = 0, r = 0, n = this.get2DCursorLocation(), s = this._textLines[n.lineIndex].split(""), o = this._getLineLeftOffset(this._getLineWidth(this.ctx, n.lineIndex)), a = 0; a < n.charIndex; a++)
            r += this._getWidthOfChar(this.ctx, s[a], n.lineIndex, a); for (a = 0; a < n.lineIndex; a++)
            i += this._getHeightOfLine(this.ctx, a); return "cursor" === e && (i += (1 - this._fontSizeFraction) * this._getHeightOfLine(this.ctx, n.lineIndex) / this.lineHeight - this.getCurrentCharFontSize(n.lineIndex, n.charIndex) * (1 - this._fontSizeFraction)), { top: i, left: r, lineLeft: o }; }, getMinWidth: function () { return Math.max(this.minWidth, this.dynamicMinWidth); }, toObject: function (t) { return e.util.object.extend(this.callSuper("toObject", t), { minWidth: this.minWidth }); } }), e.Textbox.fromObject = function (t, r) { var n = new e.Textbox(t.text, i(t)); return r && r(n), n; }, e.Textbox.getTextboxControlVisibility = function () { return { tl: !1, tr: !1, br: !1, bl: !1, ml: !0, mt: !1, mr: !0, mb: !1, mtr: !0 }; };
}("undefined" != typeof exports ? exports : this), function () { var t = fabric.Canvas.prototype._setObjectScale; fabric.Canvas.prototype._setObjectScale = function (e, i, r, n, s, o, a) { var h = i.target; if (!(h instanceof fabric.Textbox))
    return t.call(fabric.Canvas.prototype, e, i, r, n, s, o, a); var c = h.width * (e.x / i.scaleX / (h.width + h.strokeWidth)); return c >= h.getMinWidth() ? (h.set("width", c), !0) : void 0; }, fabric.Group.prototype._refreshControlsVisibility = function () { if ("undefined" != typeof fabric.Textbox)
    for (var t = this._objects.length; t--;)
        if (this._objects[t] instanceof fabric.Textbox)
            return void this.setControlsVisibility(fabric.Textbox.getTextboxControlVisibility()); }; var e = fabric.util.object.clone; fabric.util.object.extend(fabric.Textbox.prototype, { _removeExtraneousStyles: function () { for (var t in this._styleMap)
        this._textLines[t] || delete this.styles[this._styleMap[t].line]; }, insertCharStyleObject: function (t, e, i) { var r = this._styleMap[t]; t = r.line, e = r.offset + e, fabric.IText.prototype.insertCharStyleObject.apply(this, [t, e, i]); }, insertNewlineStyleObject: function (t, e, i) { var r = this._styleMap[t]; t = r.line, e = r.offset + e, fabric.IText.prototype.insertNewlineStyleObject.apply(this, [t, e, i]); }, shiftLineStyles: function (t, i) { var r = e(this.styles), n = this._styleMap[t]; t = n.line; for (var s in this.styles) {
        var o = parseInt(s, 10);
        o > t && (this.styles[o + i] = r[o], r[o - i] || delete this.styles[o]);
    } }, _getTextOnPreviousLine: function (t) { for (var e = this._textLines[t - 1]; this._styleMap[t - 2] && this._styleMap[t - 2].line === this._styleMap[t - 1].line;)
        e = this._textLines[t - 2] + e, t--; return e; }, removeStyleObject: function (t, e) { var i = this.get2DCursorLocation(e), r = this._styleMap[i.lineIndex], n = r.line, s = r.offset + i.charIndex; this._removeStyleObject(t, i, n, s); } }); }(), function () { var t = fabric.IText.prototype._getNewSelectionStartFromOffset; fabric.IText.prototype._getNewSelectionStartFromOffset = function (e, i, r, n, s) { n = t.call(this, e, i, r, n, s); for (var o = 0, a = 0, h = 0; h < this._textLines.length && (o += this._textLines[h].length, !(o + a >= n)); h++)
    "\n" !== this.text[o + a] && " " !== this.text[o + a] || a++; return n - h + a; }; }(), function () { function request(t, e, i) { var r = URL.parse(t); r.port || (r.port = 0 === r.protocol.indexOf("https:") ? 443 : 80); var n = 0 === r.protocol.indexOf("https:") ? HTTPS : HTTP, s = n.request({ hostname: r.hostname, port: r.port, path: r.path, method: "GET" }, function (t) { var r = ""; e && t.setEncoding(e), t.on("end", function () { i(r); }), t.on("data", function (e) { 200 === t.statusCode && (r += e); }); }); s.on("error", function (t) { t.errno === process.ECONNREFUSED ? fabric.log("ECONNREFUSED: connection refused to " + r.hostname + ":" + r.port) : fabric.log(t.message), i(null); }), s.end(); } function requestFs(t, e) { var i = require("fs"); i.readFile(t, function (t, i) { if (t)
    throw fabric.log(t), t; e(i); }); } if ("undefined" == typeof document || "undefined" == typeof window) {
    var DOMParser = require("xmldom").DOMParser, URL = require("url"), HTTP = require("http"), HTTPS = require("https"), Canvas = require("canvas"), Image = require("canvas").Image;
    fabric.util.loadImage = function (t, e, i) { function r(r) { r ? (n.src = new Buffer(r, "binary"), n._src = t, e && e.call(i, n)) : (n = null, e && e.call(i, null, !0)); } var n = new Image; t && (t instanceof Buffer || 0 === t.indexOf("data")) ? (n.src = n._src = t, e && e.call(i, n)) : t && 0 !== t.indexOf("http") ? requestFs(t, r) : t ? request(t, "binary", r) : e && e.call(i, t); }, fabric.loadSVGFromURL = function (t, e, i) { t = t.replace(/^\n\s*/, "").replace(/\?.*$/, "").trim(), 0 !== t.indexOf("http") ? requestFs(t, function (t) { fabric.loadSVGFromString(t.toString(), e, i); }) : request(t, "", function (t) { fabric.loadSVGFromString(t, e, i); }); }, fabric.loadSVGFromString = function (t, e, i) { var r = (new DOMParser).parseFromString(t); fabric.parseSVGDocument(r.documentElement, function (t, i) { e && e(t, i); }, i); }, fabric.util.getScript = function (url, callback) { request(url, "", function (body) { eval(body), callback && callback(); }); }, fabric.createCanvasForNode = function (t, e, i, r) { r = r || i; var n = fabric.document.createElement("canvas"), s = new Canvas(t || 600, e || 600, r), o = new Canvas(t || 600, e || 600, r); n.style = {}, n.width = s.width, n.height = s.height, i = i || {}, i.nodeCanvas = s, i.nodeCacheCanvas = o; var a = fabric.Canvas || fabric.StaticCanvas, h = new a(n, i); return h.nodeCanvas = s, h.nodeCacheCanvas = o, h.contextContainer = s.getContext("2d"), h.contextCache = o.getContext("2d"), h.Font = Canvas.Font, h; };
    var originaInitStatic = fabric.StaticCanvas.prototype._initStatic;
    fabric.StaticCanvas.prototype._initStatic = function (t, e) { t = t || fabric.document.createElement("canvas"), this.nodeCanvas = new Canvas(t.width, t.height), this.nodeCacheCanvas = new Canvas(t.width, t.height), originaInitStatic.call(this, t, e), this.contextContainer = this.nodeCanvas.getContext("2d"), this.contextCache = this.nodeCacheCanvas.getContext("2d"), this.Font = Canvas.Font; }, fabric.StaticCanvas.prototype.createPNGStream = function () { return this.nodeCanvas.createPNGStream(); }, fabric.StaticCanvas.prototype.createJPEGStream = function (t) { return this.nodeCanvas.createJPEGStream(t); }, fabric.StaticCanvas.prototype._initRetinaScaling = function () { if (this._isRetinaScaling())
        return this.lowerCanvasEl.setAttribute("width", this.width * fabric.devicePixelRatio), this.lowerCanvasEl.setAttribute("height", this.height * fabric.devicePixelRatio), this.nodeCanvas.width = this.width * fabric.devicePixelRatio, this.nodeCanvas.height = this.height * fabric.devicePixelRatio, this.contextContainer.scale(fabric.devicePixelRatio, fabric.devicePixelRatio), this; }, fabric.Canvas && (fabric.Canvas.prototype._initRetinaScaling = fabric.StaticCanvas.prototype._initRetinaScaling);
    var origSetBackstoreDimension = fabric.StaticCanvas.prototype._setBackstoreDimension;
    fabric.StaticCanvas.prototype._setBackstoreDimension = function (t, e) { return origSetBackstoreDimension.call(this, t, e), this.nodeCanvas[t] = e, this; }, fabric.Canvas && (fabric.Canvas.prototype._setBackstoreDimension = fabric.StaticCanvas.prototype._setBackstoreDimension);
} }();
var tablatureStyle = {
    fallback: {
        fontFamily: "Segoe UI"
    },
    page: {
        width: 800,
        height: 1200
    },
    bar: {
        lineHeight: 12,
        beamThickness: 4,
        beamSpacing: 4
    },
    note: {
        circleOnLongNotes: true,
        longNoteCirclePadding: 1,
        dot: {
            radius: 1.5,
            offset: 3,
            spacing: 2
        },
        flagSpacing: 4,
        tuplet: {
            fontSize: 12,
            fontFamily: "Times New Roman",
            fontStyle: "italic"
        }
    },
    tie: {
        instructionOffset: 24,
        instructionText: {
            fontSize: 12,
            fontFamily: "Times New Roman",
            fontStyle: "italic"
        }
    },
    title: {
        fontSize: 32,
        fontFamily: "Felix Titling"
    },
    fretNumber: {
        fontSize: 12,
        fontFamily: "Segoe UI"
    },
    lyrics: {
        fontSize: 13,
        fontFamily: "Times New Roman"
    }
};
var renderer;
window.onerror = function (errorMessage, url, lineNumber) {
    alert(errorMessage + "\n" + url + "#" + lineNumber);
};
window.onload = function () {
    var canvas = document.getElementById("staff");
    //let fabricCanvas = new fabric.StaticCanvas(canvas, tablatureStyle.page);
    var fabricCanvas = new fabric.Canvas(canvas, tablatureStyle.page);
    renderer = new TR.PrimitiveRenderer(fabricCanvas, tablatureStyle);
    //renderer.drawFretNumber("2", 100, 100, true);
    //renderer.drawTitle("test!!!", 400, 100);
    //renderer.drawBarLine(Core.MusicTheory.BarLine.BeginAndEndRepeat, 100, 100);
    //renderer.drawFlag(BaseNoteValue.SixtyFourth, 100, 100, OffBarDirection.Top);
    //renderer.drawTuplet("3", 100, 100);
    //renderer.drawTie(100, 300, 100, OffBarDirection.Top);
};
var Core;
(function (Core) {
    var MusicTheory;
    (function (MusicTheory) {
        (function (BarLine) {
            BarLine[BarLine["Standard"] = 0] = "Standard";
            BarLine[BarLine["Double"] = 1] = "Double";
            BarLine[BarLine["End"] = 2] = "End";
            BarLine[BarLine["BeginRepeat"] = 3] = "BeginRepeat";
            BarLine[BarLine["EndRepeat"] = 4] = "EndRepeat";
            BarLine[BarLine["BeginAndEndRepeat"] = 5] = "BeginAndEndRepeat";
            BarLine[BarLine["BeginRepeatAndEnd"] = 6] = "BeginRepeatAndEnd";
        })(MusicTheory.BarLine || (MusicTheory.BarLine = {}));
        var BarLine = MusicTheory.BarLine;
    })(MusicTheory = Core.MusicTheory || (Core.MusicTheory = {}));
})(Core || (Core = {}));
var Core;
(function (Core) {
    var MusicTheory;
    (function (MusicTheory) {
        (function (BaseNoteValue) {
            BaseNoteValue[BaseNoteValue["Large"] = 3] = "Large";
            BaseNoteValue[BaseNoteValue["Long"] = 2] = "Long";
            BaseNoteValue[BaseNoteValue["Double"] = 1] = "Double";
            BaseNoteValue[BaseNoteValue["Whole"] = 0] = "Whole";
            BaseNoteValue[BaseNoteValue["Half"] = -1] = "Half";
            BaseNoteValue[BaseNoteValue["Quater"] = -2] = "Quater";
            BaseNoteValue[BaseNoteValue["Eighth"] = -3] = "Eighth";
            BaseNoteValue[BaseNoteValue["Sixteenth"] = -4] = "Sixteenth";
            BaseNoteValue[BaseNoteValue["ThirtySecond"] = -5] = "ThirtySecond";
            BaseNoteValue[BaseNoteValue["SixtyFourth"] = -6] = "SixtyFourth";
            BaseNoteValue[BaseNoteValue["HundredTwentyEighth"] = -7] = "HundredTwentyEighth";
            BaseNoteValue[BaseNoteValue["TwoHundredFiftySixth"] = -8] = "TwoHundredFiftySixth";
        })(MusicTheory.BaseNoteValue || (MusicTheory.BaseNoteValue = {}));
        var BaseNoteValue = MusicTheory.BaseNoteValue;
    })(MusicTheory = Core.MusicTheory || (Core.MusicTheory = {}));
})(Core || (Core = {}));
var Core;
(function (Core) {
    var MusicTheory;
    (function (MusicTheory) {
        (function (GlissDirection) {
            GlissDirection[GlissDirection["FromHigher"] = 0] = "FromHigher";
            GlissDirection[GlissDirection["FromLower"] = 1] = "FromLower";
            GlissDirection[GlissDirection["ToHigher"] = 2] = "ToHigher";
            GlissDirection[GlissDirection["ToLower"] = 3] = "ToLower";
        })(MusicTheory.GlissDirection || (MusicTheory.GlissDirection = {}));
        var GlissDirection = MusicTheory.GlissDirection;
    })(MusicTheory = Core.MusicTheory || (Core.MusicTheory = {}));
})(Core || (Core = {}));
var Core;
(function (Core) {
    var MusicTheory;
    (function (MusicTheory) {
        (function (NoteValueAugment) {
            NoteValueAugment[NoteValueAugment["None"] = 0] = "None";
            NoteValueAugment[NoteValueAugment["Dot"] = 1] = "Dot";
            NoteValueAugment[NoteValueAugment["TwoDots"] = 2] = "TwoDots";
            NoteValueAugment[NoteValueAugment["ThreeDots"] = 3] = "ThreeDots";
        })(MusicTheory.NoteValueAugment || (MusicTheory.NoteValueAugment = {}));
        var NoteValueAugment = MusicTheory.NoteValueAugment;
    })(MusicTheory = Core.MusicTheory || (Core.MusicTheory = {}));
})(Core || (Core = {}));
var Core;
(function (Core) {
    var MusicTheory;
    (function (MusicTheory) {
        (function (OffBarDirection) {
            OffBarDirection[OffBarDirection["Top"] = 0] = "Top";
            OffBarDirection[OffBarDirection["Bottom"] = 1] = "Bottom";
        })(MusicTheory.OffBarDirection || (MusicTheory.OffBarDirection = {}));
        var OffBarDirection = MusicTheory.OffBarDirection;
    })(MusicTheory = Core.MusicTheory || (Core.MusicTheory = {}));
})(Core || (Core = {}));
// object.Assign implementation
if (typeof Object.assign != 'function') {
    (function () {
        Object.assign = function (target) {
            'use strict';
            if (target === undefined || target === null) {
                throw new TypeError('Cannot convert undefined or null to object');
            }
            var output = Object(target);
            for (var index = 1; index < arguments.length; index++) {
                var source = arguments[index];
                if (source !== undefined && source !== null) {
                    for (var nextKey in source) {
                        if (source.hasOwnProperty(nextKey)) {
                            output[nextKey] = source[nextKey];
                        }
                    }
                }
            }
            return output;
        };
    })();
}
var ResourceManager = (function () {
    function ResourceManager() {
    }
    ResourceManager.getTablatureResource = function (name) {
        return "resources/tablature/" + name;
    };
    ResourceManager.referenceBarSpacing = 12;
    return ResourceManager;
}());
var BarLine = Core.MusicTheory.BarLine;
var BaseNoteValue = Core.MusicTheory.BaseNoteValue;
var OffBarDirection = Core.MusicTheory.OffBarDirection;
var NoteValueAugment = Core.MusicTheory.NoteValueAugment;
var GlissDirection = Core.MusicTheory.GlissDirection;
var TR;
(function (TR) {
    var PrimitiveRenderer = (function () {
        function PrimitiveRenderer(canvas, style) {
            this.canvas = canvas;
            this.style = style;
            this.clear();
        }
        PrimitiveRenderer.prototype.clear = function () {
            this.canvas.clear();
            this.canvas.backgroundColor = "white";
        };
        PrimitiveRenderer.prototype.drawTitle = function (title, x, y) {
            var text = new fabric.Text(title, this.style.title);
            text.left = x;
            text.top = y;
            text.originX = "center";
            text.originY = "top";
            this.canvas.add(text);
        };
        PrimitiveRenderer.prototype.drawSpecialFretting = function (imageFile, x, y, isHalfOrLonger) {
            var _this = this;
            this.drawSVGFromURL(imageFile, x, y, function (group) {
                group.scaleY = _this.style.bar.lineHeight / ResourceManager.referenceBarSpacing;
                group.originX = "center";
                group.originY = "center";
                if (isHalfOrLonger && _this.style.note.circleOnLongNotes) {
                    _this.drawCircleAroundLongNote(x, y, group.getBoundingRect());
                }
            });
        };
        PrimitiveRenderer.prototype.drawFretNumber = function (fretNumber, x, y, isHalfOrLonger) {
            var text = new fabric.Text(fretNumber, this.style.fretNumber);
            text.left = x;
            text.top = y;
            text.originX = "center";
            text.originY = "center";
            this.canvas.add(text);
            if (isHalfOrLonger && this.style.note.circleOnLongNotes) {
                this.drawCircleAroundLongNote(x, y, text.getBoundingRect());
            }
        };
        PrimitiveRenderer.prototype.drawCircleAroundLongNote = function (x, y, bounds) {
            var radius = Math.max(bounds.width, bounds.height) / 2 + this.style.note.longNoteCirclePadding;
            var circle = new fabric.Circle({
                radius: radius,
                left: x,
                top: y,
                originX: "center",
                originY: "center",
                stroke: "black",
                fill: ""
            });
            this.canvas.add(circle);
        };
        PrimitiveRenderer.prototype.drawDeadNote = function (x, y, isHalfOrLonger) {
            this.drawSpecialFretting(ResourceManager.getTablatureResource("dead_note.svg"), x, y, isHalfOrLonger);
        };
        PrimitiveRenderer.prototype.drawPlayToChordMark = function (x, y, isHalfOrLonger) {
            this.drawSpecialFretting(ResourceManager.getTablatureResource("play_to_chord_mark.svg"), x, y, isHalfOrLonger);
        };
        PrimitiveRenderer.prototype.drawLyrics = function (lyrics, x, y) {
            var text = new fabric.Text(lyrics, this.style.lyrics);
            text.left = x;
            text.top = y;
            text.originX = "left";
            text.originY = "top";
            this.canvas.add(text);
        };
        PrimitiveRenderer.prototype.drawTuplet = function (tuplet, x, y) {
            var text = new fabric.Text(tuplet, this.style.note.tuplet);
            text.left = x;
            text.top = y;
            text.originX = "center";
            text.originY = "center";
            this.canvas.add(text);
        };
        PrimitiveRenderer.prototype.drawLine = function (x1, y1, x2, y2) {
            var line = new fabric.Line([x1, y1, x2, y2]);
            line.stroke = "black";
            this.canvas.add(line);
            return line;
        };
        PrimitiveRenderer.prototype.drawHorizontalBarLine = function (x, y, length) {
            this.drawLine(x, y, x + length, y);
        };
        PrimitiveRenderer.prototype.drawBarLine = function (barLine, x, y) {
            var _this = this;
            var imageFile;
            switch (barLine) {
                case BarLine.Standard:
                    imageFile = ResourceManager.getTablatureResource("barline_standard.svg");
                    break;
                case BarLine.BeginAndEndRepeat:
                    imageFile = ResourceManager.getTablatureResource("barline_begin_and_end_repeat.svg");
                    break;
                case BarLine.BeginRepeat:
                    imageFile = ResourceManager.getTablatureResource("barline_begin_repeat.svg");
                    break;
                case BarLine.BeginRepeatAndEnd:
                    imageFile = ResourceManager.getTablatureResource("barline_begin_repeat_and_end.svg");
                    break;
                case BarLine.Double:
                    imageFile = ResourceManager.getTablatureResource("barline_double.svg");
                    break;
                case BarLine.End:
                    imageFile = ResourceManager.getTablatureResource("barline_end.svg");
                    break;
                case BarLine.EndRepeat:
                    imageFile = ResourceManager.getTablatureResource("barline_end_repeat.svg");
                    break;
            }
            this.drawSVGFromURL(imageFile, x, y, function (group) {
                group.scaleToHeight(_this.style.bar.lineHeight * 5);
            });
        };
        PrimitiveRenderer.prototype.drawStem = function (x, yFrom, yTo) {
            this.drawLine(x, yFrom, x, yTo);
        };
        PrimitiveRenderer.prototype.drawSVGFromURL = function (url, x, y, callback) {
            var _this = this;
            fabric.loadSVGFromURL(url, function (results, options) {
                var group = fabric.util.groupSVGElements(results, options);
                group.left = x;
                group.top = y;
                if (callback != null)
                    callback(group);
                _this.canvas.add(group);
            });
        };
        PrimitiveRenderer.prototype.drawFlag = function (noteValue, x, y, direction) {
            var _this = this;
            if (noteValue > BaseNoteValue.Eighth)
                return;
            var flagFile = ResourceManager.getTablatureResource("note_flag.svg");
            fabric.loadSVGFromURL(flagFile, function (results, options) {
                var group = fabric.util.groupSVGElements(results, options);
                group.left = x;
                group.originX = "left";
                group.originY = "center";
                group.scale(_this.style.bar.lineHeight / ResourceManager.referenceBarSpacing);
                if (direction == OffBarDirection.Bottom)
                    group.flipY = true;
                for (var i = noteValue; i < BaseNoteValue.Quater; ++i) {
                    if (i === noteValue) {
                        group.top = y;
                        _this.canvas.add(group);
                    }
                    else {
                        group.clone(function (result) {
                            result.top = y;
                            _this.canvas.add(result);
                        });
                    }
                    y += 6;
                }
            });
        };
        PrimitiveRenderer.prototype.drawBeam = function (x1, y1, x2, y2) {
            var halfThickness = this.style.bar.beamThickness / 2;
            var points = [
                { x: x1, y: y1 - halfThickness },
                { x: x2, y: y2 - halfThickness },
                { x: x2, y: y2 + halfThickness },
                { x: x1, y: y1 + halfThickness }
            ];
            var polygon = new fabric.Polygon(points);
            polygon.fill = "black";
            polygon.stroke = "black";
            this.canvas.add(polygon);
        };
        PrimitiveRenderer.prototype.drawNoteValueAugment = function (augment, x, y) {
            for (var i = 0; i < augment; ++i) {
                var dot = new fabric.Circle({
                    radius: this.style.note.dot.radius,
                    left: x,
                    top: y,
                    originX: "left",
                    originY: "center",
                    stroke: "",
                    fill: "black"
                });
                this.canvas.add(dot);
                x += this.style.note.dot.radius * 2 + this.style.note.dot.spacing;
            }
        };
        PrimitiveRenderer.prototype.drawRest = function (noteValue, x, y) {
            var _this = this;
            var imageFile;
            switch (noteValue) {
                case BaseNoteValue.Large:
                case BaseNoteValue.Long:
                case BaseNoteValue.Double:
                case BaseNoteValue.Whole:
                case BaseNoteValue.Half:
                    imageFile = ResourceManager.getTablatureResource("rest_2.svg");
                    break;
                case BaseNoteValue.Quater:
                    imageFile = ResourceManager.getTablatureResource("rest_4.svg");
                    break;
                case BaseNoteValue.Eighth:
                    imageFile = ResourceManager.getTablatureResource("rest_8.svg");
                    break;
                case BaseNoteValue.Sixteenth:
                    imageFile = ResourceManager.getTablatureResource("rest_16.svg");
                    break;
                case BaseNoteValue.ThirtySecond:
                    imageFile = ResourceManager.getTablatureResource("rest_32.svg");
                    break;
                case BaseNoteValue.SixtyFourth:
                    imageFile = ResourceManager.getTablatureResource("rest_64.svg");
                    break;
                case BaseNoteValue.HundredTwentyEighth:
                    imageFile = ResourceManager.getTablatureResource("rest_128.svg");
                    break;
                case BaseNoteValue.TwoHundredFiftySixth:
                    imageFile = ResourceManager.getTablatureResource("rest_256.svg");
                    break;
            }
            this.drawSVGFromURL(imageFile, x, y, function (group) {
                group.originX = "center";
                group.originY = "center";
                group.scale(_this.style.bar.lineHeight / ResourceManager.referenceBarSpacing);
            });
        };
        PrimitiveRenderer.prototype.drawTie = function (x0, x1, y, instruction, instructionY, direction) {
            var _this = this;
            var imageFile = ResourceManager.getTablatureResource("tie.svg");
            this.drawSVGFromURL(imageFile, x0, y, function (group) {
                group.scaleToWidth(x1 - x0);
                group.scaleY = _this.style.bar.lineHeight / ResourceManager.referenceBarSpacing;
                if (direction == OffBarDirection.Bottom) {
                    group.originY = "top";
                    group.originX = "right";
                    group.flipY = true;
                }
                else {
                    group.originX = "left";
                    group.originY = "bottom";
                }
            });
            if (instruction != null) {
                var text = new fabric.Text(instruction, this.style.tie.instructionText);
                text.left = (x0 + x1) / 2;
                text.top = instructionY;
                text.originX = "center";
                text.originY = "center";
                this.canvas.add(text);
            }
        };
        PrimitiveRenderer.prototype.drawGliss = function (x, y, direction, instructionY) {
            var _this = this;
            var imageFile = ResourceManager.getTablatureResource("gliss.svg");
            this.drawSVGFromURL(imageFile, x, y, function (group) {
                switch (direction) {
                    case GlissDirection.FromHigher:
                        group.flipX = true;
                        group.flipY = true;
                        group.originX = "right";
                        group.originY = "bottom";
                        break;
                    case GlissDirection.FromLower:
                        group.flipX = true;
                        group.originX = "right";
                        group.originY = "top";
                        break;
                    case GlissDirection.ToHigher:
                        group.flipY = true;
                        group.originX = "left";
                        group.originY = "bottom";
                        break;
                    case GlissDirection.ToLower:
                        group.originX = "left";
                        group.originY = "top";
                        break;
                }
                group.scaleY = _this.style.bar.lineHeight / ResourceManager.referenceBarSpacing;
                var text = new fabric.Text("gl.", _this.style.tie.instructionText);
                var instructionX = x;
                switch (direction) {
                    case GlissDirection.FromHigher:
                    case GlissDirection.FromLower:
                        instructionX -= group.width / 2;
                        break;
                    case GlissDirection.ToHigher:
                    case GlissDirection.ToLower:
                        instructionX += group.width / 2;
                        break;
                }
                text.left = instructionX;
                text.top = instructionY;
                text.originX = "center";
                text.originY = "center";
                _this.canvas.add(text);
            });
        };
        return PrimitiveRenderer;
    }());
    TR.PrimitiveRenderer = PrimitiveRenderer;
})(TR || (TR = {}));
var TR;
(function (TR) {
    var TabRenderer = (function () {
        function TabRenderer(canvas, ITablatureStyle) {
            this.ITablatureStyle = ITablatureStyle;
            this.canvas = new fabric.StaticCanvas(canvas);
            this.canvas.setDimensions(this.ITablatureStyle.page);
        }
        return TabRenderer;
    }());
    TR.TabRenderer = TabRenderer;
})(TR || (TR = {}));
//# sourceMappingURL=main.js.map