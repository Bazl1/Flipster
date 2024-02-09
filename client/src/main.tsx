import ReactDOM from "react-dom/client";
import App from "./App.tsx";
import "./shared/assets/styles/global.scss";
import { Provider } from "react-redux";
import { store } from "./store/index.ts";

ReactDOM.createRoot(document.getElementById("root")!).render(
    <Provider store={store}>
        <App />
    </Provider>,
);
