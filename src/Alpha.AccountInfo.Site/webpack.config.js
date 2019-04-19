const MiniCssExtractPlugin = require("mini-css-extract-plugin");

module.exports = {
    entry: { 
        "./SearchAccount/Bundles/index": [
            "babel-polyfill",
            "./Features/SearchAccount/Scripts/app/index.jsx",
            "./Features/SearchAccount/Styles/account-info.scss"
        ],
        "./SearchAccount/Bundles/given-credit": [
            "babel-polyfill",
            "./Features/SearchAccount/Scripts/app/given-credit.jsx",
            "./Features/SearchAccount/Styles/given-credit.scss"
        ],
        "./ProductStatusPopup/Bundles/index": [
            "./Features/ProductStatusPopup/Scripts/product-status-popup.ts",
            "./Features/ProductStatusPopup/Styles/product-status-popup.scss"
        ],
        "./SearchAccount/Bundles/account-info-mobile-calculate-07": [
            "./Features/SearchAccount/Styles/account-info-mobile-calculate-07.scss"
		],
		"./UnsuspendDownlines/Bundles/unsuspend-downlines": [
			"./Features/UnsuspendDownlines/Scripts/app/unsuspend-downlines.jsx"
		],
		"./UnsuspendDownlines/Bundles/unsuspend-downlines-multiple": [
			"./Features/UnsuspendDownlines/Scripts/app/unsuspend-downlines-multiple.jsx"
		],
		"./UnsuspendDownlines/Bundles/unspend-list": [
			"./Features/UnsuspendDownlines/Scripts/app/unspend-list.jsx"
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