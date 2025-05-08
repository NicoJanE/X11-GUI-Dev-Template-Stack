# Create a Wheel
Compiled Rust library as a callable package for Python 
``maturin build --release``


# Install the wheel 
``pip install target/wheels/*.whl``

# Call the Rust function in Python
``python -c "import rust_lib; print(rust_lib.add(3, 7))"``