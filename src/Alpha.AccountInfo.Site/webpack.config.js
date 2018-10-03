const MiniCssExtractPlugin = require("mini-css-extract-plugin");

module.exports = {
    entry: {
        "bundles/account-info/index": [
            "./assets/scripts/account-info/index.jsx",
            "./assets/styles/account-info.scss"
        ],
        "bundles/account-info/given-credit": [
            "./assets/scripts/account-info/given-credit.jsx",
            "./assets/styles/given-credit.scss"
        ],
        "bundles/productstatus/productstatus": [
            "./assets/scripts/productstatus/productstatus.ts",
            "./assets/styles/productstatus/productstatus.scss"
        ],
        "bundles/styles/account-info-mobile-calculate-07": [
            "./assets/styles/account-info-mobile-calculate-07.scss"
        ]
    },

    resolve: {
        // Add '.ts' and '.tsx' as resolvable extensions.
        extensions: [".ts", ".tsx", ".js", '.jsx', ".json"]
    },

    module: {
        rules: [
            // All files with a '.ts' or '.tsx' extension will be handled by 'awesome-typescript-loader'.
            { test: /\.tsx?$/, loader: "awesome-typescript-loader" },

            {
                test: /\.jsx?$/,
                exclude: /node_modules/,
                loaders: ['babel-loader?presets[]=react,presets[]=es2015']
            },
            // All output '.js' files will have any sourcemaps re-processed by 'source-map-loader'.
            { enforce: "pre", test: /\.js$/, loader: "source-map-loader" },
            {
                test: /\.(s*)css$/,
                use: [
                    MiniCssExtractPlugin.loader,
                    "css-loader",
                    'sass-loader'
                ]
            }
        ]
    },

    // When importing a module whose path matches one of the following, just
    // assume a corresponding global variable exists and use that instead.
    // This is important because it allows us to avoid bundling all of our
    // dependencies, which allows browsers to cache those libraries between builds.
    externals: {}
};