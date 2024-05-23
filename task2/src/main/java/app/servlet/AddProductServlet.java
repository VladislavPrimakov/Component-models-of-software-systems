package app.servlet;

import app.service.PlacementService;
import app.service.ProductService;
import app.service.Result;
import app.utils.JSPUtil;
import jakarta.ejb.EJB;
import jakarta.servlet.ServletException;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

import java.io.IOException;
import java.util.Optional;

@WebServlet("/add-product")
public class AddProductServlet extends HttpServlet {
    @EJB
    private PlacementService placementService;
    @EJB
    private ProductService productService;

    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        req.setAttribute("placements", placementService.getPlacements());
        req.getRequestDispatcher(JSPUtil.getPath("/add-product")).forward(req, resp);
    }

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        Result<Boolean> result = productService.createProduct(
                Optional.ofNullable(req.getParameter("name")),
                Optional.ofNullable(req.getParameter("count")),
                Optional.ofNullable(req.getParameter("price")),
                Optional.ofNullable(req.getParameter("expiration")),
                Optional.ofNullable(req.getParameter("placementID"))
        );
        if (result.hasErrors()) {
            req.setAttribute("errors", result.getErrors());
            doGet(req, resp);
        } else {
            resp.sendRedirect("/add-product");
        }
    }
}
