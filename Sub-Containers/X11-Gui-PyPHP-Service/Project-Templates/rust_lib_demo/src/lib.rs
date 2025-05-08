use pyo3::prelude::*;

/// A simple function that adds two numbers in Rust and is callable from Python
#[pyfunction]
fn add(a: i32, b: i32) -> i32 {
    a + b
}

/// Create a Python module and expose functions
#[pymodule]
fn rust_lib(py: Python, m: &PyModule) -> PyResult<()> {
    m.add_function(wrap_pyfunction!(add, m)?)?;
    Ok(())
}

