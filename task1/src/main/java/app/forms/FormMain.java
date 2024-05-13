package app.forms;

import app.entity.User;
import app.entity.UserRole;
import app.service.TrainService;

import javax.swing.*;
import javax.swing.border.EmptyBorder;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.function.Function;

public class FormMain extends JFrame{
    private JPanel panelMain;
    private JLabel labelUsername;
    private JLabel labelRole;
    private JPanel panelButtons;
    User user;

    public FormMain(User user){
        this.user = user;

        setTitle("Main");
        setContentPane(panelMain);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setSize(800, 800);
        setVisible(true);

        panelButtons.setLayout(new BoxLayout(panelButtons, BoxLayout.Y_AXIS));
        labelUsername.setText("Username: " + user.getName());
        labelRole.setText("Role: " + user.getRole().name());

        if (user.getRole().equals(UserRole.ADMIN)){
            setAdminButtons();
        }
        else {
            setUserButtons();
        }
        repaint();
        revalidate();
    }

    public void setUserButtons(){
        JButton buttonBookingTickets = new JButton("Booking tickets");
        JButton buttonViewBookedTickets = new JButton("View booked tickets");


        buttonBookingTickets.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                new FormBookingTickets(user);
            }
        });
        buttonViewBookedTickets.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                new FormViewBookedTickets(user);
            }
        });

        panelButtons.add(buttonBookingTickets);
        panelButtons.add(Box.createRigidArea(new Dimension(0, 10)));
        panelButtons.add(buttonViewBookedTickets);
    }

    public void setAdminButtons(){
        JButton buttonAddTrains = new JButton("Add trains");
        buttonAddTrains.setSize(200,50);
        JButton buttonViewTrains = new JButton("View trains");

        buttonAddTrains.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                new FormAddTrain();
            }
        });
        buttonViewTrains.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                new FormViewTrains();
            }
        });

        panelButtons.add(buttonAddTrains);
        panelButtons.add(Box.createRigidArea(new Dimension(0, 10)));
        panelButtons.add(buttonViewTrains);
    }
}
