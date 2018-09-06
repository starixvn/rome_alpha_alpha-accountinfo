const base64js = require('base64-js');

export const base64Helper = {
    encode: (str, encoding = 'utf-8') => {
        let bytes = new (TextEncoder || TextEncoderLite)(encoding).encode(str);
        return base64js.fromByteArray(bytes);
    },
    decode: (str, encoding = 'utf-8') => {
        let bytes = base64js.toByteArray(str);
        return new (TextDecoder || TextDecoderLite)(encoding).decode(bytes);
    },
    decodeResources: (resources) => {
        for (let key in resources) {
            resources[key] = base64Helper.decode(resources[key]);
        }

        return resources;
    }
};