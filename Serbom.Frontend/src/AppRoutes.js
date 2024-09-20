import { Contract }  from "./components/Contract";
import { Home } from "./components/Home";

const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/contract/:id',
        element: <Contract action="view" />
    },
    {
        path: '/contract/new',
        element: <Contract action="new" />
    },
    {
        path: '/contract/:id/edit',
        element: <Contract action="edit" />
    }
];

export default AppRoutes;
