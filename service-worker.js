/* Manifest version: WMyBpR3X */
// حدد اسم الكاش عشان تقدر تحدثه لاحقًا لما تعمل نشر جديد
const cacheNamePrefix = 'offline-cache-';
const cacheName = `${cacheNamePrefix}v1`;
const base = self.registration.scope;
const baseUrl = new URL(base);
const manifestUrlList = self.assetsManifest.assets
    .filter(asset => !asset.url.includes('.gz'))
    .map(asset => `${baseUrl}${asset.url}`);

async function onInstall() {
    const cache = await caches.open(cacheName);
    await cache.addAll(manifestUrlList);
}

async function onActivate() {
    const cacheKeys = await caches.keys();
    await Promise.all(cacheKeys
        .filter(key => key.startsWith(cacheNamePrefix) && key !== cacheName)
        .map(key => caches.delete(key)));
}

async function onFetch(event) {
    let cachedResponse = null;
    if (event.request.method === 'GET') {
        cachedResponse = await caches.match(event.request);
        if (!cachedResponse) {
            const shouldServeIndexHtml = event.request.mode === 'navigate';
            const request = shouldServeIndexHtml ? 'index.html' : event.request;
            const cache = await caches.open(cacheName);
            cachedResponse = await cache.match(request);
        }
    }
    return cachedResponse || fetch(event.request);
}

self.addEventListener('install', event => event.waitUntil(onInstall()));
self.addEventListener('activate', event => event.waitUntil(onActivate()));
self.addEventListener('fetch', event => event.respondWith(onFetch(event)));
