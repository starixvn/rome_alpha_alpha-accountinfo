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
        "./UnsuspendDownlines/Bundles/unsuspend-single": [
            "./Features/UnsuspendDownlines/Scripts/app/unsuspend-single/unsuspend-single-container.jsx",
            "./Features/UnsuspendDownlines/Scripts/app/unsuspend-view-list.jsx",
            "./Features/UnsuspendDownlines/Styles/unsuspend-downlines.scss",
            "./Features/Shared/Scripts/react-paginations/table-pagination.scss"
        ],
        "./UnsuspendDownlines/Bundles/unsuspend-multiple": [
            "./Features/UnsuspendDownlines/Scripts/app/unsuspend-multiple/unsuspend-multiple-container.jsx",
            "./Features/UnsuspendDownlines/Scripts/app/unsuspend-view-list.jsx",
            "./Features/UnsuspendDownlines/Styles/unsuspend-downlines.scss",
            "./Features/Shared/Scripts/react-paginations/table-pagination.scss"
        ],
        "./ProfilePicture/Bundles/react-crop-images": [
            "./Features/ProfilePicture/Scripts/app/react-crop-images.jsx",
            "./node_modules/react-image-crop/dist/ReactCrop.css"
        ],
        "./ProfilePicture/Bundles/index": [
            "./Features/ProfilePicture/Styles/profile-picture.scss",
            "./Features/ProfilePicture/Scripts/app/index.jsx",
        ],
        "./ProfilePicture/Bundles/picture-library": [
            "./Features/ProfilePicture/Styles/picture-library.scss",
            "./Features/ProfilePicture/Scripts/app/picture-library.jsx",
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