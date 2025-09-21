import { RouterProvider } from "react-router-dom";
import "./App.css";
import "./responsive.css";
import router from "./routes";

function App() {
  return <RouterProvider router={router} />;
}

export default App;
