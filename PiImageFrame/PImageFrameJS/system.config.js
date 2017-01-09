System.config({
    meta: { '*.js': { scriptLoad: true } },
    packages: {
        app: {
            format: 'amd',
            defaultExtension: 'js'
        }
    },
    map: {
        moment: 'js/moment.js'
    }
});

System.import('app/boot')
              .then(null, console.error.bind(console));