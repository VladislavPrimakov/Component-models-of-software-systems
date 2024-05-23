package app.servlet;

import app.entity.Product;
import app.service.PlacementService;
import app.service.ProductService;
import app.utils.JSPUtil;
import jakarta.ejb.EJB;
import jakarta.servlet.ServletException;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

import java.io.IOException;
import java.util.List;
import java.util.Optional;

@WebServlet("/products")
public class ViewProductsServlet extends HttpServlet {
    @EJB
    private ProductService productService;
    @EJB
    private PlacementService placementService;


    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        List<Product> products = productService.getAllProducts(
                Optional.ofNullable(req.getParameter("inStock")),
                Optional.ofNullable(req.getParameter("expired")),
                Optional.ofNullable(req.getParameter("placementID"))
                );
        req.setAttribute("products", products);
        req.setAttribute("placements", placementService.getPlacements());
        req.getRequestDispatcher(JSPUtil.getPath("products")).forward(req, resp);
    }
}
