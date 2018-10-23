const MiniCssExtractPlugin = require("mini-css-extract-plugin");

module.exports = {
    entry: { 
        "bundles/account-info/index": [
            "./Features/SearchAccount/Scripts/index.jsx",
            "./Features/SearchAccount/Styles/account-info.scss"
        ],
        "bundles/account-info/given-credit": [
            "./Features/SearchAccount/Scripts/given-credit.jsx",
            "./Features/SearchAccount/Styles/given-credit.scss"
        ],
        "bundles/product-status-popup/index": [
            "./Features/ProductStatusPopup/Scripts/product-status-popup.ts",
            "./Features/ProductStatusPopup/Styles/product-status-popup.scss"
        ],
        "bundles/styles/account-info-mobile-calculate-07": [
            "./Features/SearchAccount/Styles/account-info-mobile-calculate-07.scss"
        ]
    },

    resolve: {
        // Add '.ts' and '.tsx' as resolvable extensions.
        extensions: [".ts", ".tsx", ".js", ".jsx", ".json"]
    },

    module: {
        rules: [
            // All files with a '.ts' or '.tsx' extension will be handled by 'awesome-typescript-loader'.
            {
                test: /\.tsx?$/,
                loader: "awesome-typescript-loader"
            },
            {
                test: /\.jsx?$/,
                exclude: /node_modules/,
                loader: 'babel-loader',
                query: {
                    presets: ['es2015', 'react'],
                    plugins: ['transform-object-rest-spread']
                }
            },
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