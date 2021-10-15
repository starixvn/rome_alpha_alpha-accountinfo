const MiniCssExtractPlugin = require("mini-css-extract-plugin");

module.exports = {
    entry: { 
        "./SearchAccount/Bundles/index": [
            "babel-polyfill",
            "./Features/SearchAccount/Scripts/app/index.jsx",
            "./Features/Shared/Styles/tippy.scss",
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
        "./UnsuspendDownlines/Bundles/unsuspend-single": [
            "babel-polyfill",
            "./Features/UnsuspendDownlines/Scripts/app/unsuspend-single/unsuspend-single-container.jsx",
            "./Features/UnsuspendDownlines/Scripts/app/unsuspend-view-list.jsx",
            "./Features/UnsuspendDownlines/Styles/unsuspend-downlines.scss",
            "./Features/Shared/Scripts/react-paginations/table-pagination.scss"
        ],
        "./UnsuspendDownlines/Bundles/unsuspend-multiple": [
            "babel-polyfill",
            "./Features/UnsuspendDownlines/Scripts/app/unsuspend-multiple/unsuspend-multiple-container.jsx",
            "./Features/UnsuspendDownlines/Scripts/app/unsuspend-view-list.jsx",
            "./Features/UnsuspendDownlines/Styles/unsuspend-downlines.scss",
            "./Features/Shared/Scripts/react-paginations/table-pagination.scss"
        ],
        "./ProfilePicture/Bundles/index": [
            "./node_modules/react-image-crop/dist/ReactCrop.css",
            "./Features/ProfilePicture/Styles/index.scss",
            "./Features/ProfilePicture/Scripts/app/index.jsx",
        ],
        "./OpenDownlines/Bundles/open-single": [
            "babel-polyfill",
            "./Features/OpenDownlines/Scripts/app/open-single/open-single-container.jsx",
            "./Features/OpenDownlines/Scripts/app/open-view-list.jsx",
            "./Features/UnsuspendDownlines/Styles/unsuspend-downlines.scss",
            "./Features/Shared/Scripts/react-paginations/table-pagination.scss"
        ],
        "./OpenDownlines/Bundles/open-multiple": [
            "babel-polyfill",
            "./Features/OpenDownlines/Scripts/app/open-multiple/open-multiple-container.jsx",
            "./Features/OpenDownlines/Scripts/app/open-view-list.jsx",
            "./Features/UnsuspendDownlines/Styles/unsuspend-downlines.scss",
            "./Features/Shared/Scripts/react-paginations/table-pagination.scss"
        ],
        "./ProtectedStatus/Bundles/protected-multiple": [
            "babel-polyfill",
            "./Features/ProtectedStatus/Scripts/app/protected-multiple-container.jsx",
            "./Features/ProtectedStatus/Styles/protected-status.scss"
        ],
        "./SearchAccount/Bundles/index-dark": [
            "babel-polyfill",
            "./Features/Shared/Styles/tippy.scss",
            "./Features/SearchAccount/Styles/account-info-dark.scss"
        ],
        "./ProfilePicture/Bundles/index-dark": [
            "./node_modules/react-image-crop/dist/ReactCrop.css",
            "./Features/ProfilePicture/Styles/index-dark.scss",
        ],
        "./UnsuspendDownlines/Bundles/unsuspend-single-dark": [
            "babel-polyfill",
            "./Features/Shared/Scripts/react-paginations/table-pagination.scss",
            "./Features/UnsuspendDownlines/Styles/unsuspend-downlines-dark.scss",
        ],
        "./SearchAccount/Bundles/given-credit-dark": [
            "babel-polyfill",
            "./Features/SearchAccount/Styles/given-credit-dark.scss"
        ],
        "./UnsuspendDownlines/Bundles/unsuspend-multiple-dark": [
            "babel-polyfill",
            "./Features/UnsuspendDownlines/Styles/unsuspend-downlines-dark.scss",
            "./Features/Shared/Scripts/react-paginations/table-pagination.scss"
        ],
        "./OpenDownlines/Bundles/open-single-dark": [
            "babel-polyfill",
            "./Features/UnsuspendDownlines/Styles/unsuspend-downlines-dark.scss",
            "./Features/Shared/Scripts/react-paginations/table-pagination.scss"
        ],
        "./OpenDownlines/Bundles/open-multiple-dark": [
            "babel-polyfill",
            "./Features/UnsuspendDownlines/Styles/unsuspend-downlines-dark.scss",
            "./Features/Shared/Scripts/react-paginations/table-pagination.scss"
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
            },
            {
                test: /\.(woff(2)?|ttf|eot|svg|png|jpg)(\?v=\d+\.\d+\.\d+)?$/,
                use: {
                    loader: "url-loader",
                    options: {
                        // Limit at 50k. Above that it emits separate files
                        limit: 50000,

                        // url-loader sets mimetype if it's passed.
                        // Without this it derives it from the file extension
                        mimetype: "application/font-woff",

                        // Output below fonts directory
                        name: "./fonts/[name].[ext]",
                    }
                },
            }
        ]
    },

    // When importing a module whose path matches one of the following, just
    // assume a corresponding global variable exists and use that instead.
    // This is important because it allows us to avoid bundling all of our
    // dependencies, which allows browsers to cache those libraries between builds.
    externals: {}
};