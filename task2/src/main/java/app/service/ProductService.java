package app.service;

import app.entity.Placement;
import app.entity.Product;
import app.utils.HebirnateUtil;
import jakarta.ejb.Lock;
import jakarta.ejb.LockType;
import jakarta.ejb.Singleton;
import jakarta.ejb.Startup;
import jakarta.persistence.EntityManager;
import jakarta.persistence.TypedQuery;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.time.format.DateTimeParseException;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@Singleton
@Startup
public class ProductService {

    @Lock(LockType.READ)
    public Result<Boolean> createProduct(Optional<String> name, Optional<String> count, Optional<String> price, Optional<String> expiration, Optional<String> placementID) {
        Result<Boolean> result = new Result<>();
        if (name.isPresent()) {
            if (name.get().isEmpty()) {
                result.addError("Name cannot be empty");
            }
        } else {
            result.addError("Name is null");
        }
        if (count.isPresent()) {
            if (count.get().isEmpty()) {
                result.addError("Count cannot be empty");
            }
            try {
                if (Integer.parseInt(count.get()) < 0) {
                    result.addError("Count cannot be negative");
                }
            } catch (NumberFormatException e) {
                result.addError("Invalid count value");
            }
        } else {
            result.addError("Count is null");
        }
        if (price.isPresent()) {
            if (price.get().isEmpty()) {
                result.addError("Price cannot be empty");
            }
            try {
                if (Integer.parseInt(price.get()) < 0) {
                    result.addError("Price cannot be negative");
                }
            } catch (NumberFormatException e) {
                result.addError("Invalid price value");
            }
        } else {
            result.addError("Price is null");
        }
        if (expiration.isPresent()) {
            if (expiration.get().isEmpty()) {
                result.addError("Expiration cannot be empty");
            }
            try {
                LocalDate.parse(expiration.get(), DateTimeFormatter.ofPattern("yyyy-MM-dd"));
            } catch (DateTimeParseException e) {
                result.addError("Invalid expiration value");
            }
        } else {
            result.addError("Expiration is null");
        }
        if (placementID.isPresent()) {
            if (placementID.get().isEmpty()) {
                result.addError("PlacementID cannot be empty");
            }
            try {
                Integer.parseInt(placementID.get());
            } catch (NumberFormatException e) {
                result.addError("Invalid placementID value");
            }
        } else {
            result.addError("PlacementID is null");
        }
        if (result.hasErrors()) {
            return result;
        }
        EntityManager entityManager = HebirnateUtil.getEntityManager();
        entityManager.getTransaction().begin();
        Optional<Placement> placement = entityManager.createQuery("select p from Placement p where p.id = :placementID", Placement.class)
                .setParameter("placementID", Integer.parseInt(placementID.get()))
                .getResultStream()
                .findFirst();
        boolean res = false;
        if (placement.isPresent()) {
            Product product = new Product(
                    name.get(),
                    Integer.parseInt(count.get()),
                    Double.parseDouble(price.get()),
                    LocalDate.parse(expiration.get(), DateTimeFormatter.ofPattern("yyyy-MM-dd")),
                    placement.get()
            );
            entityManager.persist(product);
            res = true;
        } else {
            result.addError("Cannot find placement");
        }
        if (result.hasErrors()) {
            return result;
        }
        result.setData(res);
        entityManager.getTransaction().commit();
        return result;
    }

    @Lock(LockType.READ)
    public List<Product> getAllProducts(Optional<String> inStock, Optional<String> expired, Optional<String> placementId) {
        EntityManager entityManager = HebirnateUtil.getEntityManager();
        entityManager.getTransaction().begin();
        String conditions = "";
        try {
            if (inStock.isPresent()) {
                if (Boolean.parseBoolean(inStock.get())) {
                    conditions += " AND p.count > 0";
                }
            }
            if (expired.isPresent()) {
                if (Boolean.parseBoolean(expired.get())) {
                    conditions += " AND p.expirationDate < :expirationDate";
                }
            }
            if (placementId.isPresent()) {
                if (Integer.parseInt(placementId.get()) > 0) {
                    conditions += " AND pl.id = " + Integer.parseInt(placementId.get());
                }
            }
            if (conditions != "") {
                conditions = " where " + conditions.substring(4);
            }
        } catch (NumberFormatException e) {
            return new ArrayList<Product>();
        }
        TypedQuery<Product> query = entityManager.createQuery("select p from Product p join p.placement pl" + conditions, Product.class);
        if (expired.isPresent()) {
            if (Boolean.parseBoolean(expired.get())) {
                query.setParameter("expirationDate", LocalDate.now().plusDays(2));
            }
        }
        List<Product> products = query.getResultList();

        entityManager.getTransaction().commit();
        return products;
    }

    @Lock(LockType.READ)
    public static boolean removeProductById(Optional<String> id) {
        if (id.isPresent()) {
            if (id.get().isEmpty()) {
                return false;
            }
            try {
                Integer.parseInt(id.get());
            } catch (NumberFormatException e) {
                return false;
            }
        }
        EntityManager entityManager = HebirnateUtil.getEntityManager();
        entityManager.getTransaction().begin();
        Optional<Product> product = entityManager.createQuery("select p from Product p where id = :id", Product.class)
                .setParameter("id", Integer.parseInt(id.get()))
                .getResultStream()
                .findFirst();
        boolean res = false;
        if (product.isPresent()) {
            entityManager.remove(product.get());
            res = true;
        }
        entityManager.getTransaction().commit();
        return res;
    }

    @Lock(LockType.READ)
    public boolean updateProductById(Optional<String> id, Optional<String> count) {
        if (id.isPresent()) {
            if (id.get().isEmpty()) {
                return false;
            }
            try {
                Integer.parseInt(id.get());
            } catch (NumberFormatException e) {
                return false;
            }
        }
        if (count.isPresent()) {
            if (count.get().isEmpty()) {
                return false;
            }
            try {
                Integer.parseInt(count.get());
            } catch (NumberFormatException e) {
                return false;
            }
            if (Integer.parseInt(count.get()) < 0){
                return false;
            }
        }
        EntityManager entityManager = HebirnateUtil.getEntityManager();
        entityManager.getTransaction().begin();
        Optional<Product> product = entityManager.createQuery("select p from Product p where id = :id", Product.class)
                .setParameter("id", Integer.parseInt(id.get()))
                .getResultStream()
                .findFirst();
        boolean res = false;
        if (product.isPresent()) {
            Product prod = product.get();
            prod.setCount(Integer.parseInt(count.get()));
            entityManager.merge(prod);
            res = true;
        }
        entityManager.getTransaction().commit();
        return res;
    }
}
