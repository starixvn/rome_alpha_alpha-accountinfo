var path = require("path");
var webpack = require("webpack");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const OptimizeCSSAssetsPlugin = require("optimize-css-assets-webpack-plugin");

module.exports = {
    entry: {
        "bundles/account-info/index": [
            "./assets/scripts/account-info/index.jsx",
            "./assets/styles/vendors/swiper/swiper.css",
            "./assets/styles/account-info.css"
        ],
        "bundles/account-info/given-credit": [
            "./assets/scripts/account-info/given-credit.jsx",
            "./assets/styles/given-credit.css"
        ],
        "bundles/productstatus/productstatus": [
            "./assets/scripts/productstatus/productstatus.ts",
            "./assets/styles/productstatus/productstatus.css"
        ]
    },

    output: {
        path: path.resolve(__dirname, 'assets'),
        filename: '[name].js'
    },

    // Enable sourcemaps for debugging webpack's output.
    devtool: "source-map",

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
                loaders: ['babel-loader?presets[]=react,presets[]=es2015'],
            },
            // All output '.js' files will have any sourcemaps re-processed by 'source-map-loader'.
            { enforce: "pre", test: /\.js$/, loader: "source-map-loader" },

            // Load css files
            {
                test: /\.(s*)css$/,
                use: [
                    'style-loader',
                    MiniCssExtractPlugin.loader,
                    "css-loader"
                ]
            },
        ]
    },

    // When importing a module whose path matches one of the following, just
    // assume a corresponding global variable exists and use that instead.
    // This is important because it allows us to avoid bundling all of our
    // dependencies, which allows browsers to cache those libraries between builds.
    externals: {
       
    },
    optimization: {
        minimizer: [
          new OptimizeCSSAssetsPlugin({})
        ]
    },
    plugins: [
        new MiniCssExtractPlugin({
            // Options similar to the same options in webpackOptions.output
            // both options are optional
            filename: "[name].css",
            chunkFilename: "[id].css"
        })
    ]
};