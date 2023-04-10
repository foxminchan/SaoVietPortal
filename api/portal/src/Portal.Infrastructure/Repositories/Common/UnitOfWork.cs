﻿using Portal.Domain.Interfaces;
using Portal.Domain.Interfaces.Common;

namespace Portal.Infrastructure.Repositories.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IStudentRepository studentRepository { get; private set; }

    public IBranchRepository branchRepository { get; private set; }

    public IClassRepository classRepository { get; private set; }

    public ICourseRepository courseRepository { get; private set; }

    public IPaymentMethodRepository paymentMethodRepository { get; private set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        studentRepository = new StudentRepository(_context);
        branchRepository = new BranchRepository(_context);
        classRepository = new ClassRepository(_context);
        courseRepository = new CourseRepository(_context);
        paymentMethodRepository = new PaymentMethodRepository(_context);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _context.Dispose();
    }
}